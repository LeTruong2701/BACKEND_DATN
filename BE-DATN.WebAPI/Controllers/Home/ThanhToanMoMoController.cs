using BE_DATN.Application.BUS.Home.DTO;
using BE_DATN.WebAPI.MoMoDev;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using Newtonsoft.Json;


namespace BE_DATN.WebAPI.Controllers.Home
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThanhToanMoMoController : ControllerBase
    {

        [Route("create-donhangmomo")]
        [HttpPost]
        public ActionResult Payment([FromBody] DonHangModel us)
        {
                      

                string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor"; // call đến trang thanh toán momo
                string partnerCode = "MOMOOJOI20210710";
                string accessKey = "iPXneGmrJH0G8FOP";    //3 cái này là key cho doanh nghiệp
                string serectkey = "sFcbSGRSJjwGxwhhcEktCHWYUuTuPNDB";
                string orderInfo = "Thanh toán tiền hàng";
                string returnUrl = "http://localhost:4200/homes/thanhtoanmomo";
                string notifyurl = "https://4c8d-2001-ee0-5045-50-58c1-b2ec-3123-740d.ap.ngrok.io/Home/SavePayment"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test

                string amount = Convert.ToString(us.TongTien);
                string orderid = DateTime.Now.Ticks.ToString(); //mã đơn hàng
                string requestId = DateTime.Now.Ticks.ToString();
                string extraData = "";

                //Before sign HMAC SHA256 signature
                string rawHash = "partnerCode=" +
                    partnerCode + "&accessKey=" +
                    accessKey + "&requestId=" +
                    requestId + "&amount=" +
                    amount + "&orderId=" +
                    orderid + "&orderInfo=" +
                    orderInfo + "&returnUrl=" +
                    returnUrl + "&notifyUrl=" +
                    notifyurl + "&extraData=" +
                    extraData;

                MoMoSecurity crypto = new MoMoSecurity();
                //sign signature SHA256
                string signature = crypto.signSHA256(rawHash, serectkey);

                //build body json request
                JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };

                string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

                JObject jmessage = JObject.Parse(responseFromMomo);
            string payUrl = jmessage.GetValue("payUrl").ToString();

            //return Redirect(jmessage.GetValue("payUrl").ToString());
            return Content(JsonConvert.SerializeObject(new { url = payUrl }), "application/json");


        }







            //[Route("create-donhangmomo")]
            //[HttpPost]
            //public ActionResult<string> Payment([FromBody] DonHangModel us)
            //{

            //    int tong = 222222;

            //        //

            //        string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor"; // call đến trang thanh toán momo
            //        string partnerCode = "MOMOOJOI20210710";
            //        string accessKey = "iPXneGmrJH0G8FOP";    //3 cái này là key cho doanh nghiệp
            //        string serectkey = "sFcbSGRSJjwGxwhhcEktCHWYUuTuPNDB";
            //        string orderInfo = "Thanh toán tiền hàng";
            //        string returnUrl = "http://localhost:4200";
            //        string notifyurl = "https://4c8d-2001-ee0-5045-50-58c1-b2ec-3123-740d.ap.ngrok.io/Home/SavePayment"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test

            //        string amount = Convert.ToString(tong);
            //        string orderid = DateTime.Now.Ticks.ToString(); //mã đơn hàng
            //        string requestId = DateTime.Now.Ticks.ToString();
            //        string extraData = "";

            //        //Before sign HMAC SHA256 signature
            //        string rawHash = "partnerCode=" +
            //            partnerCode + "&accessKey=" +
            //            accessKey + "&requestId=" +
            //            requestId + "&amount=" +
            //            amount + "&orderId=" +
            //            orderid + "&orderInfo=" +
            //            orderInfo + "&returnUrl=" +
            //            returnUrl + "&notifyUrl=" +
            //            notifyurl + "&extraData=" +
            //            extraData;

            //        MoMoSecurity crypto = new MoMoSecurity();
            //        //sign signature SHA256
            //        string signature = crypto.signSHA256(rawHash, serectkey);

            //        //build body json request
            //        JObject message = new JObject
            //        {
            //            { "partnerCode", partnerCode },
            //            { "accessKey", accessKey },
            //            { "requestId", requestId },
            //            { "amount", amount },
            //            { "orderId", orderid },
            //            { "orderInfo", orderInfo },
            //            { "returnUrl", returnUrl },
            //            { "notifyUrl", notifyurl },
            //            { "extraData", extraData },
            //            { "requestType", "captureMoMoWallet" },
            //            { "signature", signature }

            //        };

            //        string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            //        JObject jmessage = JObject.Parse(responseFromMomo);
            //        string payUrl = jmessage.GetValue("payUrl").ToString();

            //    return Redirect(jmessage.GetValue("payUrl").ToString());

            //    //return Content(JsonConvert.SerializeObject(new { url = payUrl }), "application/json");

            //}


        }
}
