using System;
using System.IO;
using System.Net;
using CMS.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Specialized;
using System.Web;
using CMS.Models.Helpers;
using System.Security.Claims;

namespace CMS.Controllers
{
    [Route("cms3/ui/{language?}")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Cookies")]
    //[Authorize(Roles = "Everyone,Anonymous")]  
    public class UIController : ControllerBase
    {




        /// <summary>
        /// 
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Produces("text/html")]
        //public ContentResult Index(string language, bool logout = false)
        public ContentResult Index(string language = "ar")
        {
            try
            {



                if (string.IsNullOrEmpty(language))
                {
                    var vategoryhtml = System.IO.File.ReadAllText(Config.WWWPath + "category.html");

                    return new ContentResult
                    {
                        ContentType = "text/html",
                        StatusCode = (int)HttpStatusCode.OK,
                        Content = vategoryhtml
                    };
                }
                //Added by Kalam on 26/01/2020, for load the site based on selected category
                if (Request.QueryString.HasValue)
                {
                    NameValueCollection qscoll = HttpUtility.ParseQueryString(Request.QueryString.Value);
                    if (qscoll.GetValues("category") != null)
                        CMS.Models.clsCategory.selectedCategory = qscoll.GetValues("category")[0].ToString();
                }

                var html = System.IO.File.ReadAllText(Config.WWWPath + "index.html");
                /*if(logout)
                {
                    CMS.User.logout();
                    //IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    //authenticationManager.SignOut(MyAuthentication.ApplicationCookie);
                    SignOut();
                    html = System.IO.File.ReadAllText(Config.WWWPath + "/logout.html");
                }*/
                //Reading the html file


                //Changing the HTML file to match the selected language
                html = html
                    .Replace("[[LG]]", language ?? "ar");

                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = html
                };

            }
            catch (Exception e)
            {

                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = "<p>" + e.Message + "</p>" /// vategoryhtml
                };
            }
        }

        [AllowAnonymous]
        [HttpGet("{siteCode}")]
        [Produces("text/html")]
        public ContentResult List(string language, string siteCode)
        {
            var site = Site.GetSite(siteCode);

            if (!site.canRead)
            {
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Content = "<html>Not Authorized</html>"
                };
            }

            var html = System.IO.File.ReadAllText(Config.WWWPath + "list.master.html");

            html = html
                .Replace("<!-- <#Include components/list_header.html> -->", System.IO.File.ReadAllText(Config.WWWPath + "components/list_header.html"))
                .Replace("[[SITE]]", siteCode)
                .Replace("[[LG]]", language ?? "ar");

            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = html
            };
        }


        [AllowAnonymous]
        [HttpGet("{siteCode}/{contentCode}/{contentVersion}/view")]
        [Produces("text/html")]
        public ContentResult View(string language , string siteCode, string contentCode, string contentVersion)
        {
            try
            {

           
                    
            var content = CMS.Models.Content.GetContent(siteCode, contentCode, contentVersion);

            if ((!content.CanRead))
            {
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Content = "<html>Not Authorized</html>"
                };
            }

            var html = System.IO.File.ReadAllText(Config.WWWPath + "view.master.html");

            html = html
                .Replace("<!-- <#Include components/view_header.html> -->", System.IO.File.ReadAllText(Config.WWWPath + "components/view_header.html"))
                .Replace("<!-- <#Include components/view_versions.html> -->", System.IO.File.ReadAllText(Config.WWWPath + "components/view_versions.html"))
                .Replace("<!-- <#Include components/view_comments.html> -->", System.IO.File.ReadAllText(Config.WWWPath + "components/view_comments.html"))
                .Replace("<!-- <#Include forms/site/view.html> -->", System.IO.File.ReadAllText(Config.WWWPath + "forms/" + siteCode + "/view.html"))
                .Replace("[[SITE]]", siteCode)
                .Replace("[[SITENAME]]", content.site.name[language??"ar"])
                .Replace("[[CONTENT]]", contentCode)
                .Replace("[[VERSION]]", contentVersion)
                .Replace("[[LG]]", language ?? "ar");

            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = html
            };

            }
            catch (Exception e)
            {

                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Content = "<html>"+e.Message+"</html>"
                };
            }
        }

        [Authorize]
        [HttpGet("{siteCode}/{contentCode}/{contentVersion}/edit")]
        [Produces("text/html")]
        public ContentResult Edit(string language , string siteCode, string contentCode, string contentVersion)
        {
            var site = Site.GetSite(siteCode);




            if (!site.canEdit)
            {
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Content = "<html>Not Authorized</html>"
                };
            }

            var html = System.IO.File.ReadAllText(Config.WWWPath + "edit.master.html");

            html = html
                .Replace("<!-- <#Include components/edit_header.html> -->", System.IO.File.ReadAllText(Config.WWWPath + "components/edit_header.html"))
                .Replace("<!-- <#Include components/edit_attachments.html> -->", System.IO.File.ReadAllText(Config.WWWPath + "components/edit_attachments.html"))
                .Replace("<!-- <#Include components/edit_relations.html> -->", System.IO.File.ReadAllText(Config.WWWPath + "components/edit_relations.html"))
                // .Replace("<!-- <#Include forms/site/edit.html> -->", System.IO.File.ReadAllText(Config.WWWPath + "forms/" + siteCode + "/edit.html"))
                .Replace("<!-- <#Include forms/site/edit.html> -->", sbForEditPage) //System.IO.File.ReadAllText(Config.BaseWWWPath + "/cms3/forms/" + siteCode + "/edit.html"))
                .Replace("[[SITE]]", siteCode)
                .Replace("[[CONTENT]]", contentCode)
                .Replace("[[VERSION]]", contentVersion)
                .Replace("[[SITENAME]]", site.name[language ?? "ar"])
                .Replace("[[LG]]", language ?? "ar");

            //replace lookup options
            Regex filter = new Regex(@"\<\!\-\- \<\#Lookup ([^>]+)\> \-\-\>");

            foreach (Match match in filter.Matches(html))
            {
                var lookupCode = match.Groups[1].Value.Trim();

                var lookupSite = ContentSummaryList.GetList(lookupCode, (!site.canEdit || HttpContext.Request.Cookies["user_Lognstatus"] == null));

                var options = "";

                foreach (var contentSummary in lookupSite.contents)
                {
                    options += $"<option value=\"{contentSummary.code}\">{contentSummary.code} | {contentSummary.subject[language ?? "ar"]}</option>\r\n";
                }

                html = html.Replace(match.Value, options);
            }

            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = html
            };
        }

        [AllowAnonymous]
        [HttpGet("{siteCode}/{contentCode}/{contentVersion}/download/{fileName}")]
        public FileResult download(string language, string siteCode, string contentCode, string contentVersion, string fileName)
        {
            var content = CMS.Models.Content.GetContent(siteCode, contentCode, contentVersion);

             var filePath = content.attachmentsPath + fileName;

          //  byte[] fileBytes = System.IO.File.ReadAllBytes(HttpUtility.UrlEncode(filePath));

            byte[] fileBytesData = System.IO.File.ReadAllBytes(@""+ filePath + "");
           
            return File(fileBytesData, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);


         //   return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }


        //[AllowAnonymous]
        //[HttpGet("Logout")]
        //public async Task<IActionResult> Logout()
        //{
        //    string spth = Path.Combine(Directory.GetCurrentDirectory());
        //    //var url = HttpContext.Request.Host+ "/category.html";
        //    var url = "category.html";
        //    Response.Cookies.Delete("user_Lognstatus");
        //    await HttpContext.SignOutAsync(
        //                CookieAuthenticationDefaults.AuthenticationScheme);

        //    return new OkObjectResult(new { url = url, success = "true" });
        //    //return;//this.Redirect(url);           
        //}


        [AllowAnonymous]
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            validate ldabV1 = new validate();
            var url = "/cms3/login";
            Response.Cookies.Delete("user_Lognstatus");

            DleteCookie("user_Lognstatus", "");
            DleteCookie("CMS", "");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme, new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow
            });

            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (result.Succeeded)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, HttpContext.User, new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(-1),
                    IsPersistent = false
                });
            }
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("user_Lognstatus");
            HttpContext.Session.Remove("CMS");
            ldabV1.UnValidateUser();
            var resultx = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            bool test = resultx.Succeeded;

            /// HttpContext.Session["MyName"] = "C-SharpCorner";
            return new OkObjectResult(new { url = url, success = "true" });
            //return;//this.Redirect(url);           
        }


        public void DleteCookie(string key, string value)
        {
            CookieOptions option = new CookieOptions();
            //if (expireTime.HasValue)
            option.Expires = new DateTimeOffset(DateTime.Now.AddDays(-1)); //DateTime.Now.AddMinutes(10);
            option.IsEssential = true;
            //else
            //	option.Expires = new DateTimeOffset(DateTime.Now.AddMinutes(expireTime)); //DateTime.Now.AddMinutes(60);
            Response.Cookies.Append(key, value, option);
        }














        string sbForEditPage = "<div class=\"col-md-12\">" +
"    <div class=\"form-group\">" +
"        <label  data-i18n=\"Start_the_service_link\" > رابط ابدأ الخدمة</label>" +
"        " +
"        <input class=\"form-control\" style=\"direction:ltr;text-align:left;\" v-model=\"content.body.serviceUrl_[[LG]]\" />" +
"    </div>" +
"    <div class=\"form-group\">" +
"        <label data-i18n=\"Download_the_user_guide_link\">رابط دليل المستخدم</label>" +
"        <input class=\"form-control\" style=\"direction:ltr;text-align:left;\" v-model=\"content.body.guidelineURL_[[LG]]\" />" +
"    </div>" +
"    </div>" +
"    <br />" +
"    <div class=\"col-md-12\">" +
"        <ul class=\"tabs-edit\">" +
"            <li><a href=\"javascript:toggleTab('.requirment_1')\" onclick=\"activeTo(this)\" class=\"active\" data-i18n=\"requirements_first_entity\">متطلبات الجهة الاولى</a></li>" +
"            <li><a href=\"javascript:toggleTab('.requirment_2')\" onclick=\"activeTo(this)\"  data-i18n=\"requirements_second_entity\">متطلبات الجهة الثانية</a></li>" +
"            <li><a href=\"javascript:toggleTab('.requirment_3')\" onclick=\"activeTo(this)\"  data-i18n=\"requirements_third_entity\">متطلبات الجهة الثالثة</a></li>" +
"        </ul>" +
"        <div class=\"requirment_1 h-tabs block\">" +
"            <input type=\"hidden\" v-model=\"content.body.requirements1.attachmentKey\" value=\"REQ1\" />" +
"            <div class=\"form-group\">" +
"                <label  data-i18n=\"The_name_of_the_entity\">اسم الجهة</label>" +
"                <input type=\"text\" class=\"form-control\" v-model=\"content.body.requirements1.[[LG]].name\" />" +
"            </div>" +
"            <div class=\"row\">" +
"                <div class=\"form-group col-6\">" +
"                    <label><img src=\"/cms3/new/assets/images/icons/time.svg\" width=\"30\" > <span data-i18n=\"Duration_of_service\"></span></label>" +
"                    <input type=\"text\" class=\"form-control\" v-model=\"content.body.requirements1.[[LG]].serviceTime\" />" +
"                </div>" +
"                <div class=\"form-group col-6\">" +
"                    <label><img src=\"/cms3/new/assets/images/icons/cost.svg\" width=\"24\" > <span data-i18n=\"Service_fee\"></span></label>" +
"                    <input type=\"number\"  onkeyup=\"this.value=this.value.replace(/[^\\d]/,'')\"  class=\"form-control\" v-model=\"content.body.requirements1.[[LG]].serviceCost\" />" +
"                </div>" +
"            </div>" +
"            <div class=\"form-group\">" +
"                <label><img src=\"/cms3/new/assets/images/icons/exponantial.svg\"> <span data-i18n=\"Service_description\"></span></label>" +
"                <quill-editor class=\"editorRich\" ref=\"quillEditor\" v-model=\"content.body.requirements1.[[LG]].serviceDescription\"" +
"                              :options=\"editorOption\">" +
"                </quill-editor>" +
"            </div>" +
"            <div class=\"form-group\">" +
"                <label><img src=\"/cms3/new/assets/images/icons/people.svg\"> <span data-i18n=\"Beneficiary_category\"></span></label>" +
"                <quill-editor class=\"editorRich\" ref=\"quillEditor\" v-model=\"content.body.requirements1.[[LG]].BeneficiaryCategory\"" +
"                              :options=\"editorOption\">" +
"                </quill-editor>" +
"            </div>" +
"            <div class=\"form-group\">" +
"                <label><img src=\"/cms3/new/assets/images/icons/settings.svg\"> <span data-i18n=\"How_to_use_the_service\"></span></label>" +
"                <quill-editor class=\"editorRich\" ref=\"quillEditor\" v-model=\"content.body.requirements1.[[LG]].serviceUse\"" +
"                              :options=\"editorOption\">" +
"                </quill-editor>" +
"            </div>" +
"            <div class=\"form-group\">" +
"                <label><img src=\"/cms3/new/assets/images/icons/person_with_quastion_mark.svg\"> <span data-i18n=\"Initial_requirements\"></span></label>" +
"                <quill-editor class=\"editorRich\" ref=\"quillEditor\" v-model=\"content.body.requirements1.[[LG]].BasicRequirment\"" +
"                              :options=\"editorOption\">" +
"                </quill-editor>" +
"            </div>" +
"        </div>" +
"        <div class=\"requirment_2 h-tabs\">" +
"            <input type=\"hidden\" v-model=\"content.body.requirements2.attachmentKey\" value=\"REQ2\" />" +
"            <div class=\"form-group\">" +
"                <label data-i18n=\"The_name_of_the_entity\">اسم الجهة</label>" +
"                <input type=\"text\" class=\"form-control\" v-model=\"content.body.requirements2.[[LG]].name\" />" +
"            </div>" +
"            <div class=\"row\">" +
"                <div class=\"form-group col-6\">" +
"                    <label><img src=\"/cms3/new/assets/images/icons/time.svg\" width=\"30\"> <span data-i18n=\"Duration_of_service\"></span></label>" +
"                    <input type=\"text\" class=\"form-control\" v-model=\"content.body.requirements2.[[LG]].serviceTime\" />" +
"                </div>" +
"                <div class=\"form-group col-6\">" +
"                    <label><img src=\"/cms3/new/assets/images/icons/cost.svg\" width=\"24\"> <span data-i18n=\"Service_fee\"></span></label>" +
"                    <input type=\"number\"  onkeyup=\"this.value=this.value.replace(/[^\\d]/,'')\" class=\"form-control\" v-model=\"content.body.requirements2.[[LG]].serviceCost\" />" +
"                </div>" +
"            </div>" +
"            <div class=\"form-group\">" +
"                <label><img src=\"/cms3/new/assets/images/icons/exponantial.svg\"> <span data-i18n=\"Service_description\"></span></label>" +
"                <quill-editor class=\"editorRich\" ref=\"quillEditor\" v-model=\"content.body.requirements2.[[LG]].serviceDescription\"" +
"                              :options=\"editorOption\">" +
"                </quill-editor>" +
"            </div>" +
"            <div class=\"form-group\">" +
"                <label><img src=\"/cms3/new/assets/images/icons/people.svg\"> <span data-i18n=\"Beneficiary_category\"></span></label>" +
"                <quill-editor class=\"editorRich\" ref=\"quillEditor\" v-model=\"content.body.requirements2.[[LG]].BeneficiaryCategory\"" +
"                              :options=\"editorOption\">" +
"                </quill-editor>" +
"            </div>" +
"            <div class=\"form-group\">" +
"                <label><img src=\"/cms3/new/assets/images/icons/settings.svg\"> <span data-i18n=\"How_to_use_the_service\"></span></label>" +
"                <quill-editor class=\"editorRich\" ref=\"quillEditor\" v-model=\"content.body.requirements2.[[LG]].serviceUse\"" +
"                              :options=\"editorOption\">" +
"                </quill-editor>" +
"            </div>" +
"            <div class=\"form-group\">" +
"                <label><img src=\"/cms3/new/assets/images/icons/person_with_quastion_mark.svg\"><span data-i18n=\"Initial_requirements\"></span></label>" +
"                <quill-editor class=\"editorRich\" ref=\"quillEditor\" v-model=\"content.body.requirements2.[[LG]].BasicRequirment\"" +
"                              :options=\"editorOption\">" +
"                </quill-editor>" +
"            </div>" +
"        </div>" +
"        <div class=\"requirment_3 h-tabs\">" +
"            <input type=\"hidden\" v-model=\"content.body.requirements3.attachmentKey\" value=\"REQ3\" />" +
"            <div class=\"form-group\">" +
"                <label data-i18n=\"The_name_of_the_entity\">اسم الجهة</label>" +
"                <input type=\"text\" class=\"form-control\" v-model=\"content.body.requirements3.[[LG]].name\" />" +
"            </div>" +
"            <div class=\"row\">" +
"                <div class=\"form-group col-6\">" +
"                    <label><img src=\"/cms3/new/assets/images/icons/time.svg\" width=\"30\"> <span data-i18n=\"Duration_of_service\"></span></label>" +
"                    <input type=\"text\" class=\"form-control\" v-model=\"content.body.requirements3.[[LG]].serviceTime\" />" +
"                </div>" +
"                <div class=\"form-group col-6\">" +
"                    <label><img src=\"/cms3/new/assets/images/icons/cost.svg\" width=\"24\"> <span data-i18n=\"Service_fee\"></span></label>" +
"                    <input type=\"number\"  onkeyup=\"this.value=this.value.replace(/[^\\d]/,'')\"  class=\"form-control\" v-model=\"content.body.requirements3.[[LG]].serviceCost\" />" +
"                </div>" +
"            </div>" +
"            <div class=\"form-group\">" +
"                <label><img src=\"/cms3/new/assets/images/icons/exponantial.svg\"><span data-i18n=\"Service_description\"></span></label>" +
"           " +
"                <quill-editor class=\"editorRich\" ref=\"quillEditor\" v-model=\"content.body.requirements3.[[LG]].serviceDescription\"" +
"                              :options=\"editorOption\">" +
"                </quill-editor>" +
"            </div>" +
"            <div class=\"form-group\">" +
"                <label><img src=\"/cms3/new/assets/images/icons/people.svg\"><span data-i18n=\"Beneficiary_category\"></span></label>" +
"                <quill-editor class=\"editorRich\" ref=\"quillEditor\" v-model=\"content.body.requirements3.[[LG]].BeneficiaryCategory\"" +
"                              :options=\"editorOption\">" +
"                </quill-editor>" +
"            </div>" +
"            <div class=\"form-group\">" +
"                <label><img src=\"/cms3/new/assets/images/icons/settings.svg\"> <span data-i18n=\"How_to_use_the_service\"></span></label>" +
"                <quill-editor class=\"editorRich\" ref=\"quillEditor\" v-model=\"content.body.requirements3.[[LG]].serviceUse\"" +
"                              :options=\"editorOption\">" +
"                </quill-editor>" +
"            </div>" +
"            <div class=\"form-group\">" +
"                <label><img src=\"/cms3/new/assets/images/icons/person_with_quastion_mark.svg\"> <span data-i18n=\"Initial_requirements\"></span></label>" +
"                <quill-editor class=\"editorRich\" ref=\"quillEditor\" v-model=\"content.body.requirements3.[[LG]].BasicRequirment\"" +
"                              :options=\"editorOption\">" +
"                </quill-editor>" +
"            </div>" +
"        </div>" +
"        <!--<div class=\"form-group\">" +
"            <label>OGA</label>" +
"            <select class=\"form-control\" v-model=\"content.body.oga\">" +
"                 <#Lookup OGA >" +
"            </select>" +
"        </div>-->" +
"        <!--<div class=\"form-group\">" +
"            <label>Type</label>" +
"            <select class=\"form-control\" v-model=\"content.body.type\">" +
"                <option v-for=\"proc in getTypes()\">{{proc}}</option>" +
"            </select>" +
"        </div>-->" +
"        <!--<div class=\"form-group\">" +
"            <label>Number</label>" +
"            <input type=\"number\" class=\"form-control\" v-model=\"content.body.number\" />" +
"        </div>-->" +
"        <!--<div class=\"form-group\">" +
"            <label>Beneficiary</label>" +
"            <select class=\"form-control\" v-model=\"content.body.beneficiaries\" multiple>" +
"                <option v-for=\"ben in getBeneficiaries()\">{{ben}}</option>" +
"            </select>" +
"        </div>-->" +
"        <!--<div class=\"form-group\">" +
"            <label>Duration</label>" +
"            <input type=\"text\" class=\"form-control\" v-model=\"content.body.duration.[[LG]]\" />" +
"            \"" +
"            /" +
"        </div>" +
"        <div class=\"form-group\">" +
"            <label>Fees</label>" +
"            <input type=\"number\" class=\"form-control\" v-model=\"content.body.fees\" />" +
"        </div>-->" +
"        <!-- <div class=\"form-group\">" +
"                        <label>Steps</label>" +
"                        <input type=\"text\" class=\"form-control\" v-model=\"content.body.duration\"/>" +
"                    </div> -->" +
"        <!--<div class=\"form-group\">" +
"            <label>Steps</label>" +
"            <textarea class=\"form-control\" v-model=\"content.body.steps.[[LG]]\"></textarea>" +
"        </div>" +
"        <div class=\"form-group\">" +
"            <label>Requirements</label>" +
"            <textarea class=\"form-control\" v-model=\"content.body.requirements.[[LG]]\"></textarea>" +
"        </div>-->" +
"        <!--<div class=\"form-group\">" +
"            <label>Url</label>" +
"            <input type=\"text\" class=\"form-control\" v-model=\"content.body.url.[[LG]]\" />" +
"        </div>-->" +
"    </div>";


        //[HttpGet]
        //[Produces("text/html")]
        //public ContentResult Logout(string language)
        //{
        //    CMS.User.logout();            
        //    //Reading the html file
        //    var html = System.IO.File.ReadAllText(Config.WWWPath + "/logout.html");

        //    //Changing the HTML file to match the selected language
        //    html = html
        //        .Replace("[[LG]]", language);

        //    return new ContentResult
        //    {
        //        ContentType = "text/html",
        //        StatusCode = (int)HttpStatusCode.OK,
        //        Content = html
        //    };
        //}

        //public void RaiseCallbackEvent(string eventArgument)
        //{
        //}

        //public string GetCallbackResult()
        //{
        //    return "";
        //}

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    ClearAll();
        //    ClientScriptManager cm = Page.ClientScript;
        //    string cbReference = cm.GetCallbackEventReference(this, "arg", "HandleResult", "");
        //    string cbScript = "function CallServer(arg, context){" + cbReference + ";}";
        //    cm.RegisterClientScriptBlock(this.GetType(), "CallServer", cbScript, true);
        //    cm.RegisterStartupScript(this.GetType(), "cle", "windows.history.clear", true);
        //    Response.Redirect("/cms3/login.aspx");

        //}
        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    ClearAll();
        //}

        //void ClearAll()
        //{
        //    Session.RemoveAll();
        //    System.Web.Security.FormsAuthentication.SignOut();
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
        //    Response.Cache.SetNoStore();
        //}
    }
}
