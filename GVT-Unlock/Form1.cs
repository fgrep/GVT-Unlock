using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using HttpServer;
using HttpServer.Headers;
using HttpListener = HttpServer.HttpListener;

namespace GVT_Unlock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            var certificate = new X509Certificate2("cert.p12", "");
            var listener = (SecureHttpListener)HttpListener.Create(IPAddress.Any, 443, certificate);

            GlobalVar.UnlockSent = 0;
            listener.UseClientCertificate = true;
            listener.RequestReceived += OnRequest;
            listener.Start(5);
            AppendTextBox("Servidor web iniciado.\r\n");
            AppendTextBox("Carregando página de login.\r\n");
            webBrowser1.Navigate("http://192.168.25.1/pt_BR/admin/config_lan.htm");
        }

        public void AppendTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }
            textBox1.Text += value;
        }

        public void OnRequest(object sender, RequestEventArgs e)
        {
            // Write info to the buffer.
            e.Response.Connection.Type = ConnectionType.Close;
            if (GlobalVar.UnlockSent == 0)
            {
                AppendTextBox("Modem conectado, enviando parametros para habilitar SSH e tipo de acesso 4 (desbloqueio)\r\n");
                byte[] buffer = Encoding.Default.GetBytes("<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:cwmp=\"urn:dslforum-org:cwmp-1-0\"><soap:Header><cwmp:NoMoreRequests>0</cwmp:NoMoreRequests></soap:Header><soap:Body><cwmp:SetParameterValues><ParameterList encodingStyle:arrayType=\"cwmp:ParameterValueStruct[2]\"><ParameterValueStruct><Name>InternetGatewayDevice.Services.X_Pace_Com.Services.GvtConfig.AccessClass</Name><Value>4</Value></ParameterValueStruct><ParameterValueStruct><Name>InternetGatewayDevice.Services.X_Pace_Com.Services.SSH.Enable</Name><Value>1</Value></ParameterValueStruct></ParameterList><ParameterKey></ParameterKey></cwmp:SetParameterValues></soap:Body></soap:Envelope>");
                e.Response.Body.Write(buffer, 0, buffer.Length);
                GlobalVar.UnlockSent = 1;
            }
            else if (GlobalVar.UnlockSent == 1)
            {
                byte[] buffer = Encoding.Default.GetBytes("<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"               xmlns:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\"               xmlns:cwmp=\"urn:dslforum-org:cwmp-1-0\">  <soap:Header>    <cwmp:NoMoreRequests>0</cwmp:NoMoreRequests>  </soap:Header>  <soap:Body>    <cwmp:InformResponse>      <MaxEnvelopes>1</MaxEnvelopes>    </cwmp:InformResponse>  </soap:Body></soap:Envelope>");
                e.Response.Body.Write(buffer, 0, buffer.Length);
                GlobalVar.UnlockSent = 2;
                AppendTextBox("Finalizado.\r\n");
            }
        }

        /*
        static string ConfigurarModem(string cookie)
        {
            using (var client = new WebClientEx())
            {
                var values = new NameValueCollection
            {
                { "login", "admin" },
                { "password", "gvt12345" },
                { "request", "authentication"},
                { "level", "admin" },
                { "seed", "c1e550c8801aba24b71b33ce360975a769b8aaa6030ad089a67086305289bb44" },
            };
                string url = "http://192.168.25.1/pt_BR/admin/config_lan.htm";
                client.UploadValues("http://192.168.25.1/cgi-bin/login.cgi", values);
                //client.CookieContainer.SetCookies(url, cookie);
                return client.DownloadString("http://192.168.25.1/pt_BR/admin/config_lan.htm");
            }
        }
        */

        private void button2_Click(object sender, EventArgs e)
        {
            string strSource = webBrowser1.DocumentText;
            string strStart = "token: util.parseJson(\'\"";
            if (strSource.Contains(strStart))
            {
                int Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                var token = strSource.Substring(Start, 6);
                webBrowser1.Hide();
                AppendTextBox("Token encontrado: " + token + "\r\n");
                AppendTextBox("Apontando host acs.gvt.com.br para 192.168.25.2\r\n");
                webBrowser1.Navigate("http://192.168.25.1/cgi-bin/generic.cgi?token=" + token + "&write=LANDevice_1_HostConfig_StaticHost_1_IPAddress:192.168.25.2&write=LANDevice_1_HostConfig_StaticHost_1_Hostname:acs.gvt.com.br");
                AppendTextBox("Aguarde 5 segundos e reinicie o modem para concluir o processo.");
            }
            else
            {
                MessageBox.Show("Token não encontrado, tente novamente.");
            }
        }


    }

    public static class GlobalVar
    {
        static int _unlockSent;
        public static int UnlockSent
        {
            get
            {
                return _unlockSent;
            }
            set
            {
                _unlockSent = value;
            }
        }
    }
    /*
    public class WebClientEx : WebClient
    {
        public CookieContainer CookieContainer { get; set; }

        public WebClientEx()
        {
            CookieContainer = new CookieContainer();
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).CookieContainer = CookieContainer;
            }
            return request;
        }
    }
     */

 }
