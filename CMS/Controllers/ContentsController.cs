using CMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Web.Helpers;

namespace CMS.Controllers
{
    /// <summary>
    /// SITE CONTROLLLER
    /// </summary>
    [ApiController]
    [Route("cms3/services/[controller]/{siteCode}")]
    public class ContentsController : ControllerBase
    {
        private readonly ILogger<ContentsController> _logger;
        HtmlEncoder _htmlEncoder;
        JavaScriptEncoder _javaScriptEncoder;
        UrlEncoder _urlEncoder;
        public ContentsController(ILogger<ContentsController> logger, HtmlEncoder htmlEncoder,
                             JavaScriptEncoder javascriptEncoder,
                             UrlEncoder urlEncoder)
        {
            _logger = logger;
            _htmlEncoder = htmlEncoder;
            _javaScriptEncoder = javascriptEncoder;
            _urlEncoder = urlEncoder;
        }

        ///  [System.Web.Mvc.ValidateInput(true)]
        [System.Web.Mvc.ValidateInput(true)]
        [Authorize]
        [HttpPost]
        public IActionResult Post(string siteCode, Content content)
        {

            if (!validateContentRequiredFields(content)) {

                return BadRequest("please fill all required fields");

            }
            encodeAllSpecialCharacterInConten(content);
            content.siteCode = siteCode;



            if (CMS.Models.Content.CheckIfContenteXIS(siteCode, content.code) && content.isNew)
            {

                return BadRequest("the code is already exist");
            }



            if (content.CanSave || content.CanHide || content.CanDelete || content.CanReject)
            {
                content.Save();

                return Ok(new { sucess = true, content.version });
            }
            else
            {
                return Unauthorized(new { sucess = false });
            }
            //}
            //else
            //{
            //    return Unauthorized(new { sucess = false });
            //}
        }



        public string handlingBodyEncode(string body)
        {


            var roorObjectData = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(body);

            roorObjectData.guidelineURL_ar = roorObjectData.guidelineURL_ar != null ? removeunNeedCharactersFromString(roorObjectData.guidelineURL_ar.ToLower()) : "";
            roorObjectData.serviceUrl_ar = roorObjectData.serviceUrl_ar != null ? removeunNeedCharactersFromString(roorObjectData.serviceUrl_ar.ToLower()) : "";

            roorObjectData.guidelineURL_en = roorObjectData.guidelineURL_en != null ? removeunNeedCharactersFromString(roorObjectData.guidelineURL_en.ToLower()) : "";
            roorObjectData.serviceUrl_en = roorObjectData.serviceUrl_en != null ? removeunNeedCharactersFromString(roorObjectData.serviceUrl_en.ToLower()) : "";


            ///req1
            roorObjectData.requirements1.ar.BasicRequirment = roorObjectData.requirements1.ar.BasicRequirment != null ? removeunNeedCharactersFromString(roorObjectData.requirements1.ar.BasicRequirment.ToLower()) : "";/// _javaScriptEncoder.Encode(roorObjectData.requirements1.ar.BasicRequirment):"";
            roorObjectData.requirements1.ar.BeneficiaryCategory = roorObjectData.requirements1.ar.BeneficiaryCategory != null ? removeunNeedCharactersFromString(roorObjectData.requirements1.ar.BeneficiaryCategory.ToLower()) : "";/// _javaScriptEncoder.Encode(roorObjectData.requirements1.ar.BeneficiaryCategory):"";
            roorObjectData.requirements1.ar.serviceDescription = roorObjectData.requirements1.ar.serviceDescription != null ? removeunNeedCharactersFromString(roorObjectData.requirements1.ar.serviceDescription).ToLower() : "";
            roorObjectData.requirements1.ar.serviceUse = roorObjectData.requirements1.ar.serviceUse != null ? removeunNeedCharactersFromString(roorObjectData.requirements1.ar.serviceUse.ToLower()) : "";
            roorObjectData.requirements1.ar.name = roorObjectData.requirements1.ar.name != null ? removeunNeedCharactersFromString(roorObjectData.requirements1.ar.name.ToLower()) : "";
            roorObjectData.requirements1.ar.serviceCost = roorObjectData.requirements1.ar.serviceCost != null ? removeunNeedCharactersFromString(roorObjectData.requirements1.ar.serviceCost.ToLower()) : "";
            roorObjectData.requirements1.ar.serviceTime = roorObjectData.requirements1.ar.serviceTime != null ? removeunNeedCharactersFromString(roorObjectData.requirements1.ar.serviceTime.ToLower()) : "";

            roorObjectData.requirements1.en.BasicRequirment = roorObjectData.requirements1.en.BasicRequirment != null ? removeunNeedCharactersFromString(roorObjectData.requirements1.en.BasicRequirment.ToLower()) : "";/// _javaScriptEncoder.Encode(roorObjectData.requirements1.ar.BasicRequirment):"";
            roorObjectData.requirements1.en.BeneficiaryCategory = roorObjectData.requirements1.en.BeneficiaryCategory != null ? removeunNeedCharactersFromString(roorObjectData.requirements1.en.BeneficiaryCategory.ToLower()) : "";/// _javaScriptEncoder.Encode(roorObjectData.requirements1.ar.BeneficiaryCategory):"";
            roorObjectData.requirements1.en.serviceDescription = roorObjectData.requirements1.en.serviceDescription != null ? removeunNeedCharactersFromString(roorObjectData.requirements1.en.serviceDescription).ToLower() : "";
            roorObjectData.requirements1.en.serviceUse = roorObjectData.requirements1.en.serviceUse != null ? removeunNeedCharactersFromString(roorObjectData.requirements1.en.serviceUse.ToLower()) : "";
            roorObjectData.requirements1.en.name = roorObjectData.requirements1.en.name != null ? removeunNeedCharactersFromString(roorObjectData.requirements1.en.name.ToLower()) : "";
            roorObjectData.requirements1.en.serviceCost = roorObjectData.requirements1.en.serviceCost != null ? removeunNeedCharactersFromString(roorObjectData.requirements1.en.serviceCost.ToLower()) : "";
            roorObjectData.requirements1.en.serviceTime = roorObjectData.requirements1.en.serviceTime != null ? removeunNeedCharactersFromString(roorObjectData.requirements1.en.serviceTime.ToLower()) : "";



            //req2
            roorObjectData.requirements2.ar.BasicRequirment = roorObjectData.requirements2.ar.BasicRequirment != null ? removeunNeedCharactersFromString(roorObjectData.requirements2.ar.BasicRequirment.ToLower()) : "";/// _javaScriptEncoder.Encode(roorObjectData.requirements1.ar.BasicRequirment):"";
            roorObjectData.requirements2.ar.BeneficiaryCategory = roorObjectData.requirements2.ar.BeneficiaryCategory != null ? removeunNeedCharactersFromString(roorObjectData.requirements2.ar.BeneficiaryCategory.ToLower()) : "";/// _javaScriptEncoder.Encode(roorObjectData.requirements1.ar.BeneficiaryCategory):"";
            roorObjectData.requirements2.ar.serviceDescription = roorObjectData.requirements2.ar.serviceDescription != null ? removeunNeedCharactersFromString(roorObjectData.requirements2.ar.serviceDescription).ToLower() : "";
            roorObjectData.requirements2.ar.serviceUse = roorObjectData.requirements2.ar.serviceUse != null ? removeunNeedCharactersFromString(roorObjectData.requirements2.ar.serviceUse.ToLower()) : "";
            roorObjectData.requirements2.ar.name = roorObjectData.requirements2.ar.name != null ? removeunNeedCharactersFromString(roorObjectData.requirements2.ar.name.ToLower()) : "";
            roorObjectData.requirements2.ar.serviceCost = roorObjectData.requirements2.ar.serviceCost != null ? removeunNeedCharactersFromString(roorObjectData.requirements2.ar.serviceCost.ToLower()) : "";
            roorObjectData.requirements2.ar.serviceTime = roorObjectData.requirements2.ar.serviceTime != null ? removeunNeedCharactersFromString(roorObjectData.requirements2.ar.serviceTime.ToLower()) : "";

            roorObjectData.requirements2.en.BasicRequirment = roorObjectData.requirements2.en.BasicRequirment != null ? removeunNeedCharactersFromString(roorObjectData.requirements2.en.BasicRequirment.ToLower()) : "";/// _javaScriptEncoder.Encode(roorObjectData.requirements1.ar.BasicRequirment):"";
            roorObjectData.requirements2.en.BeneficiaryCategory = roorObjectData.requirements2.en.BeneficiaryCategory != null ? removeunNeedCharactersFromString(roorObjectData.requirements2.en.BeneficiaryCategory.ToLower()) : "";/// _javaScriptEncoder.Encode(roorObjectData.requirements1.ar.BeneficiaryCategory):"";
            roorObjectData.requirements2.en.serviceDescription = roorObjectData.requirements2.en.serviceDescription != null ? removeunNeedCharactersFromString(roorObjectData.requirements2.en.serviceDescription).ToLower() : "";
            roorObjectData.requirements2.en.serviceUse = roorObjectData.requirements2.en.serviceUse != null ? removeunNeedCharactersFromString(roorObjectData.requirements2.en.serviceUse.ToLower()) : "";
            roorObjectData.requirements2.en.name = roorObjectData.requirements2.en.name != null ? removeunNeedCharactersFromString(roorObjectData.requirements2.en.name.ToLower()) : "";
            roorObjectData.requirements2.en.serviceCost = roorObjectData.requirements2.en.serviceCost != null ? removeunNeedCharactersFromString(roorObjectData.requirements2.en.serviceCost.ToLower()) : "";
            roorObjectData.requirements2.en.serviceTime = roorObjectData.requirements2.en.serviceTime != null ? removeunNeedCharactersFromString(roorObjectData.requirements2.en.serviceTime.ToLower()) : "";


            //req3
            roorObjectData.requirements3.ar.BasicRequirment = roorObjectData.requirements3.ar.BasicRequirment != null ? removeunNeedCharactersFromString(roorObjectData.requirements3.ar.BasicRequirment.ToLower()) : "";/// _javaScriptEncoder.Encode(roorObjectData.requirements1.ar.BasicRequirment):"";
            roorObjectData.requirements3.ar.BeneficiaryCategory = roorObjectData.requirements3.ar.BeneficiaryCategory != null ? removeunNeedCharactersFromString(roorObjectData.requirements3.ar.BeneficiaryCategory.ToLower()) : "";/// _javaScriptEncoder.Encode(roorObjectData.requirements1.ar.BeneficiaryCategory):"";
            roorObjectData.requirements3.ar.serviceDescription = roorObjectData.requirements3.ar.serviceDescription != null ? removeunNeedCharactersFromString(roorObjectData.requirements3.ar.serviceDescription).ToLower() : "";
            roorObjectData.requirements3.ar.serviceUse = roorObjectData.requirements3.ar.serviceUse != null ? removeunNeedCharactersFromString(roorObjectData.requirements3.ar.serviceUse.ToLower()) : "";
            roorObjectData.requirements3.ar.name = roorObjectData.requirements3.ar.name != null ? removeunNeedCharactersFromString(roorObjectData.requirements3.ar.name.ToLower()) : "";
            roorObjectData.requirements3.ar.serviceCost = roorObjectData.requirements3.ar.serviceCost != null ? removeunNeedCharactersFromString(roorObjectData.requirements3.ar.serviceCost.ToLower()) : "";
            roorObjectData.requirements3.ar.serviceTime = roorObjectData.requirements3.ar.serviceTime != null ? removeunNeedCharactersFromString(roorObjectData.requirements3.ar.serviceTime.ToLower()) : "";
           
            roorObjectData.requirements3.en.BasicRequirment = roorObjectData.requirements3.en.BasicRequirment != null ? removeunNeedCharactersFromString(roorObjectData.requirements3.en.BasicRequirment.ToLower()) : "";/// _javaScriptEncoder.Encode(roorObjectData.requirements1.ar.BasicRequirment):"";
            roorObjectData.requirements3.en.BeneficiaryCategory = roorObjectData.requirements3.en.BeneficiaryCategory != null ? removeunNeedCharactersFromString(roorObjectData.requirements3.en.BeneficiaryCategory.ToLower()) : "";/// _javaScriptEncoder.Encode(roorObjectData.requirements1.ar.BeneficiaryCategory):"";
            roorObjectData.requirements3.en.serviceDescription = roorObjectData.requirements3.en.serviceDescription != null ? removeunNeedCharactersFromString(roorObjectData.requirements3.en.serviceDescription).ToLower() : "";
            roorObjectData.requirements3.en.serviceUse = roorObjectData.requirements3.en.serviceUse != null ? removeunNeedCharactersFromString(roorObjectData.requirements3.en.serviceUse.ToLower()) : "";
            roorObjectData.requirements3.en.name = roorObjectData.requirements3.en.name != null ? removeunNeedCharactersFromString(roorObjectData.requirements3.en.name.ToLower()) : "";
            roorObjectData.requirements3.en.serviceCost = roorObjectData.requirements3.en.serviceCost != null ? removeunNeedCharactersFromString(roorObjectData.requirements3.en.serviceCost.ToLower()) : "";
            roorObjectData.requirements3.en.serviceTime = roorObjectData.requirements3.en.serviceTime != null ? removeunNeedCharactersFromString(roorObjectData.requirements3.en.serviceTime.ToLower()) : "";



            body = JsonConvert.SerializeObject(roorObjectData);

            return body;
        }


        public string removeunNeedCharactersFromString(string original)
        {

            List<string> javascriptEvent = new List<string>() { "onchange", "preventdefault", "onclick", "onmouseover", "onmouseout", "click", "onkeydown", "onload", "document", "window", "location" };
            List<string> specialCharacters = new List<string>() { "<%", "<script>", "script", "</script>", "()", "(", ")", ";", "@" };
            foreach (var item in javascriptEvent)
            {
                if (original.ToLower().Contains(item.ToLower()))
                {
                    original = original.ToLower().Replace(item, string.Empty);
                }


            }
            foreach (var item in specialCharacters)
            {
                if (original.ToLower().Contains(item.ToLower()))
                {
                    original = original.ToLower().Replace(item, string.Empty);
                }
            }
            ////
            ///then removing all special chracters that not needed 

            return original.ToLower();
        }


        public bool validateContentRequiredFields(Content content)
        {
            if ((!string.IsNullOrEmpty (content.subject.ar)&& !string.IsNullOrEmpty(content.description.ar)) || (!string.IsNullOrEmpty(content.subject.en) &&!string.IsNullOrEmpty(content.description.en)))
            {
                return true;
            }
            return false;
        }
        public void encodeAllSpecialCharacterInConten(Content content)
        {
            ///  Regex rRemScript = new Regex(@"<script[^>]*>[\s\S]*?</script>");
            content.code = content.code != null ? EncodeString(content.code) : "";
            
            content.subject.ar = content.subject.ar != null ? EncodeString(content.subject.ar.ToString()) : "";
            content.subject.en = content.subject.en != null ? EncodeString(content.subject.en.ToString()) : "";

            content.description.ar = content.description.ar != null ? EncodeString(content.description.ar.ToString()) : "";
            content.description.en = content.description.en != null ? EncodeString(content.description.en.ToString()) : "";

           
            content.body = handlingBodyEncode(content.body.ToLower()); //stripScriptTags.Replace(content.body, string.Empty);
            content.category = content.category != null ? EncodeString(content.category.ToString()) : "";
            content.siteCode = content.siteCode != null ? EncodeString(content.siteCode.ToString()) : "";

            if (content.tags.ar != null)
            {
                if (content.tags.ar.Count > 0)
                {
                    for (int i = 0; i < content.tags.ar.Count; i++)
                    {
                        content.tags.ar[i] = EncodeString(content.tags.ar[i]);
                    }
                }
            }

            if (content.tags.en != null)
            {
                if (content.tags.en.Count > 0)
                {
                    for (int i = 0; i < content.tags.en.Count; i++)
                    {
                        content.tags.en[i] = EncodeString(content.tags.en[i]);
                    }
                }
            }



            content.version = content.version != null ? EncodeString(content.version.ToString()) : "";







        }



        public string EncodeString(string ValueToEncode)
        {

            ValueToEncode = _htmlEncoder.Encode(ValueToEncode);
            //  ValueToEncode = _urlEncoder.Encode(ValueToEncode);
            ValueToEncode = _javaScriptEncoder.Encode(ValueToEncode);
            Regex rRemScript = new Regex(@"<script[^>]*>[\s\S]*?</script>");
            ValueToEncode = rRemScript.Replace(ValueToEncode, "");


            return ValueToEncode;

        }

        [Authorize]
        [System.Web.Mvc.ValidateInput(true)]
        [HttpPost("{contentCode}/{contentVersion}/publish")]
        public IActionResult Publish(string siteCode, string contentCode, string contentVersion, dynamic changesJson)
        {
            var content = Models.Content.GetContent(siteCode, contentCode, contentVersion);

            if (content.CanPublish)
            {
                content.Publish(changesJson.ToString());

                return Ok(new { success = true });
            }
            else if (content.CanRequestReview)
            {
                content.RequestReview();

                return Ok(new { success = true });
            }
            else
            {
                return Unauthorized(new { success = false });
            }
        }

        [Authorize]
        [HttpPost("{contentCode}/{contentVersion}/attach")]
        public IActionResult Attach(string siteCode, string contentCode, string contentVersion)
        {
            var content = Models.Content.GetContent(siteCode, contentCode, contentVersion);
            var count = 0;
            string FileExtension = string.Empty;
            string contentType = string.Empty;
            List<dynamic> uploaded = new List<dynamic>();

            if (content.CanSave)
            {
                if (!Directory.Exists(content.attachmentsPath))
                {
                    Directory.CreateDirectory(content.attachmentsPath);
                }


                foreach (var file in Request.Form.Files)
                {
                    FileExtension = System.IO.Path.GetExtension(file.FileName);

                    if (FileExtension.Length > 1048576)
                    {
                        uploaded.Add(new { success = false, name = file.FileName, error = "size" });
                        return Ok(uploaded);
                    }
                    if (!(FileExtension == ".pdf" || FileExtension == ".docx"))
                    {
                        uploaded.Add(new { success = false, name = file.FileName, error = "format" });
                        return Ok(uploaded);
                    }
                    var bresult = new FileExtensionContentTypeProvider().TryGetContentType(file.FileName, out contentType);

                    if (!(contentType == "application/pdf" || contentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || contentType == "application/msword"))
                    {
                        uploaded.Add(new { success = false, name = file.FileName, error = "format" });
                        return Ok(uploaded);
                    }

                    string contenttype = String.Empty;
                    Stream checkStream = file.OpenReadStream();
                    BinaryReader chkBinary = new BinaryReader(checkStream);
                    Byte[] chkbytes = chkBinary.ReadBytes(0x256);


                    string data_as_hex = BitConverter.ToString(chkbytes);
                    string magicCheck = data_as_hex.Substring(0, 11);
                    if (!(magicCheck == "50-4B-03-04" || magicCheck == "25-50-44-46"))
                    {
                        uploaded.Add(new { success = false, name = file.FileName, error = "format" });
                        return Ok(uploaded);
                    }

                    if (magicCheck == "50-4B-03-04")
                    {
                        try
                        {
                            using (WordprocessingDocument doc = WordprocessingDocument.Open(checkStream, false))
                            {
                                doc.Close();
                            }
                        }
                        catch
                        {
                            uploaded.Add(new { success = false, name = file.FileName, error = "format" });
                            return Ok(uploaded);
                        }
                    }

                }

                foreach (var file in Request.Form.Files)
                {
                    count++;

                    var tempFilePath = Path.GetTempFileName();
                    var destFilePath = content.attachmentsPath + file.FileName;

                    if (System.IO.File.Exists(destFilePath))
                    {

                        uploaded.Add(new { success = false, name = file.FileName, error = "exist" });
                        return Ok(uploaded);
                        //System.IO.File.Delete(destFilePath);
                        //using (var stream = System.IO.File.Create(tempFilePath))
                        //{
                        //    file.CopyTo(stream);

                        //    Console.WriteLine(tempFilePath);
                        //}

                        //System.IO.File.Move(tempFilePath, destFilePath, false);

                        //uploaded.Add(new { success = true, name = file.FileName });

                        ///     uploaded.Add(new { success = false, name = file.FileName, error = "file already exists, please use a differtnt name!" });
                    }
                    else
                    {
                        using (var stream = System.IO.File.Create(tempFilePath))
                        {
                            file.CopyTo(stream);

                            Console.WriteLine(tempFilePath);
                        }

                        System.IO.File.Move(tempFilePath, destFilePath, false);

                        uploaded.Add(new { success = true, name = file.FileName });
                    }
                }

                return Ok(uploaded);
            }
            else
            {
                return Unauthorized();
            }
        }

        [AllowAnonymous]
        [HttpGet("{contentCode}/{contentVersion}/{actionName}")]
        public IActionResult Get(string siteCode, string contentCode, string contentVersion, string actionName)
        {
            var content = CMS.Models.Content.GetContent(siteCode, contentCode, contentVersion);

            if (!content.site.canRead) 
            { 
                return Unauthorized(new { success = false }); 
            }
            else
            {
                System.Collections.Hashtable _links = new System.Collections.Hashtable();
                if (content.IsHide)
                {
                    if ((!content.CanReject) && (!content.CanHide) && (!content.CanDelete) && (!content.CanReadReader)) { return BadRequest("unauthorize access restricted, please close the window and contact the administrator for access permissions."); }
                    else
                    {
                        //System.Collections.Hashtable _links = new System.Collections.Hashtable();

                        if (actionName == "view")
                        {
                            if (content.CanSave)
                            {
                                _links.Add("Edit", $"{siteCode}/{contentCode}/{contentVersion}");
                            }
                            else if (content.CanCreateVersion)
                            {
                                _links.Add("Edit (New Version)", $"{siteCode}/{contentCode}/{contentVersion}/versions");
                            }

                            if (content.CanPublish)
                            {
                                _links.Add("Publish", $"{siteCode}/{contentCode}/{contentVersion}");
                            }
                            else if (content.CanRequestReview)
                            {
                                _links.Add("Submit (For Review)", $"{siteCode}/{contentCode}/{contentVersion}");
                            }

                            Response.Headers.Add("_links", Newtonsoft.Json.JsonConvert.SerializeObject(_links));
                        }
                        else if (actionName == "edit")
                        {
                            if (!content.CanSave)
                                return Unauthorized(new { success = false });
                        }

                        return Ok(new { success = false, content = content, relations = content.GetRelations(), comments = content.GetComments(), uname = CMS.User.username, uemail = CMS.User.email });
                    }
                        
                }
                if (content.IsReject)
                {
                    if ((!content.CanReject) && (!content.CanHide) && (!content.CanDelete) && (!content.CanReadReader)) { return BadRequest("unauthorize access restricted, please close the window and contact the administrator for access permissions."); }
                    else
                    {
                        //System.Collections.Hashtable _links = new System.Collections.Hashtable();

                        if (actionName == "view")
                        {
                            if (content.CanSave)
                            {
                                _links.Add("Edit", $"{siteCode}/{contentCode}/{contentVersion}");
                            }
                            else if (content.CanCreateVersion)
                            {
                                _links.Add("Edit (New Version)", $"{siteCode}/{contentCode}/{contentVersion}/versions");
                            }

                            if (content.CanPublish)
                            {
                                _links.Add("Publish", $"{siteCode}/{contentCode}/{contentVersion}");
                            }
                            else if (content.CanRequestReview)
                            {
                                _links.Add("Submit (For Review)", $"{siteCode}/{contentCode}/{contentVersion}");
                            }

                            Response.Headers.Add("_links", Newtonsoft.Json.JsonConvert.SerializeObject(_links));
                        }
                        else if (actionName == "edit")
                        {
                            if (!content.CanSave)
                                return Unauthorized(new { success = false });
                        }

                        return Ok(new { success = false, content = content, relations = content.GetRelations(), comments = content.GetComments(), uname = CMS.User.username, uemail = CMS.User.email });
                    }
                }
                if (content.IsDelete)
                {
                    if((!content.CanReject) && (!content.CanHide) && (!content.CanDelete) && (!content.CanReadReader)) { return BadRequest("unauthorize access restricted, please close the window and contact the administrator for access permissions."); }
                    else
                    {
                        //System.Collections.Hashtable _links = new System.Collections.Hashtable();

                        if (actionName == "view")
                        {
                            if (content.CanSave)
                            {
                                _links.Add("Edit", $"{siteCode}/{contentCode}/{contentVersion}");
                            }
                            else if (content.CanCreateVersion)
                            {
                                _links.Add("Edit (New Version)", $"{siteCode}/{contentCode}/{contentVersion}/versions");
                            }

                            if (content.CanPublish)
                            {
                                _links.Add("Publish", $"{siteCode}/{contentCode}/{contentVersion}");
                            }
                            else if (content.CanRequestReview)
                            {
                                _links.Add("Submit (For Review)", $"{siteCode}/{contentCode}/{contentVersion}");
                            }

                            Response.Headers.Add("_links", Newtonsoft.Json.JsonConvert.SerializeObject(_links));
                        }
                        else if (actionName == "edit")
                        {
                            if (!content.CanSave)
                                return Unauthorized(new { success = false });
                        }

                        return Ok(new { success = false, content = content, relations = content.GetRelations(), comments = content.GetComments(), uname = CMS.User.username, uemail = CMS.User.email });
                    }
                }

                

                if (actionName == "view")
                {
                    if (content.CanSave)
                    {
                        _links.Add("Edit", $"{siteCode}/{contentCode}/{contentVersion}");
                    }
                    else if (content.CanCreateVersion)
                    {
                        _links.Add("Edit (New Version)", $"{siteCode}/{contentCode}/{contentVersion}/versions");
                    }

                    if (content.CanPublish)
                    {
                        _links.Add("Publish", $"{siteCode}/{contentCode}/{contentVersion}");
                    }
                    else if (content.CanRequestReview)
                    {
                        _links.Add("Submit (For Review)", $"{siteCode}/{contentCode}/{contentVersion}");
                    }

                    Response.Headers.Add("_links", Newtonsoft.Json.JsonConvert.SerializeObject(_links));
                }
                else if (actionName == "edit")
                {
                    if (!content.CanSave)
                        return Unauthorized(new { success = false });
                }

                return Ok(new { success = false, content = content, relations = content.GetRelations(), comments = content.GetComments(), uname = CMS.User.username, uemail = CMS.User.email });
            }            
        }
    }
}
