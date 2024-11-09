using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Web;
using CSDLGia_ASP.Database;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Systems.Auth
{
    public class SSOController : Controller
    {
        private const string AuthorizationEndpoint = "https://cas.vinhlong.gov.vn/cas/oidc/authorize";
        private const string TokenEndpoint = "https://cas.vinhlong.gov.vn/cas/oidc/token";
        private const string UserInfoEndpoint = "https://cas.vinhlong.gov.vn/cas/oidc/userinfo";
        private const string ClientId = "your_client_id";
        private const string ClientSecret = "your_client_secret";
        private const string Scope = "openid profile";

        private readonly CSDLGiaDBContext _db;
        private readonly HttpClient _httpClient;

        public SSOController(CSDLGiaDBContext db, HttpClient httpClient)
        {
            _db = db;
            _httpClient = httpClient;
        }

        [HttpGet("SSO/Authorize")]
        public IActionResult Authorize()
        {
            // Lấy URL gốc của ứng dụng và tạo RedirectUri
            string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            string redirectUri = $"{baseUrl}/SSO/CallBack";  // URL callback tự động

            // Tạo state ngẫu nhiên
            var randomStateCode = Guid.NewGuid().ToString();
            var state = new Dictionary<string, object>
            {
                [randomStateCode] = new Dictionary<string, object>
                {
                    ["redirectUrl"] = "/",
                    ["expiresOn"] = DateTime.UtcNow.AddMinutes(10).ToString("o")
                }
            };

            // Chuyển state thành JSON
            string stateJson = Newtonsoft.Json.JsonConvert.SerializeObject(state);

            // Xây dựng URL để chuyển hướng người dùng đến trang xác thực
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["response_type"] = "code";
            queryString["client_id"] = ClientId;
            queryString["redirect_uri"] = redirectUri;
            queryString["scope"] = Scope;
            queryString["state"] = stateJson;

            // Chuyển hướng người dùng
            var authorizationUrl = $"{AuthorizationEndpoint}?{queryString}";
            return Redirect(authorizationUrl);
        }

        // Callback từ trang xác thực
        [HttpGet("SSO/CallBack")]
        public async Task<IActionResult> CallBack(string code, string state)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest("Authorization code is required.");
            }

            // Lấy lại redirectUri động
            string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            string redirectUri = $"{baseUrl}/SSO/CallBack";

            // Tạo nội dung yêu cầu cho token
            var requestBody = new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "code", code },
                { "redirect_uri", redirectUri },
                { "client_id", ClientId },
                { "client_secret", ClientSecret }
            };

            var requestContent = new FormUrlEncodedContent(requestBody);
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            // Gửi yêu cầu POST để lấy token
            var response = await _httpClient.PostAsync(TokenEndpoint, requestContent);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Unable to retrieve token.");
            }

            // Xử lý token từ phản hồi
            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenData = JObject.Parse(responseContent);
            var accessToken = tokenData["access_token"]?.ToString();

            //return Ok(accessToken);

            // Sử dụng access token để lấy thông tin người dùng
            var userInfoRequest = new HttpRequestMessage(HttpMethod.Get, UserInfoEndpoint);
            userInfoRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var userInfoResponse = await _httpClient.SendAsync(userInfoRequest);

            if (!userInfoResponse.IsSuccessStatusCode)
            {
                return StatusCode((int)userInfoResponse.StatusCode, "Unable to retrieve user info.");
            }

            var userInfoContent = await userInfoResponse.Content.ReadAsStringAsync();
            var userInfo = JObject.Parse(userInfoContent);

            return Ok(userInfo);
            //string username = userInfo["username"]?.ToString();
            //string email = userInfo["email"]?.ToString();
            //if (this.CheckUserInfo(username, email)) {
            //    //Insert acccout && SignIn
            //}
            //else
            //{
            //    //SignIn
            //}

        }

        public bool CheckUserInfo(string username, string email)
        {
            var model = _db.Users.FirstOrDefault(t => t.Username == username && t.Email == email && t.SSO);
            if (model != null)
            {
                return true;
            }
            return false;
        }
    }


}
