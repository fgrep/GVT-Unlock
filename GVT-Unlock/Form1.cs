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

        public void OnRequestDesbloqueio(object sender, RequestEventArgs e)
        {
            // Write info to the buffer.
            e.Response.Connection.Type = ConnectionType.Close;
            if (GlobalVar.UnlockSent == 0)
            {
                string opcionais = "";
                int numparams = 1;
                if (checkBox1.Checked)
                {
                    opcionais += "<ParameterValueStruct><Name>InternetGatewayDevice.Services.X_Pace_Com.Services.SSH.Enable</Name><Value>1</Value></ParameterValueStruct>";
                    numparams++;
                }

                if (checkBox2.Checked)
                {
                    opcionais += "<ParameterValueStruct><Name>InternetGatewayDevice.Services.X_Pace_Com.HPNA.Enable</Name><Value>1</Value></ParameterValueStruct>";
                    numparams++;
                }

                AppendTextBox("Modem conectado, enviando configuração.\r\n");
                byte[] buffer = Encoding.Default.GetBytes("<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:cwmp=\"urn:dslforum-org:cwmp-1-0\"><soap:Header><cwmp:NoMoreRequests>0</cwmp:NoMoreRequests></soap:Header><soap:Body><cwmp:SetParameterValues><ParameterList encodingStyle:arrayType=\"cwmp:ParameterValueStruct["+numparams+"]\"><ParameterValueStruct><Name>InternetGatewayDevice.Services.X_Pace_Com.Services.GvtConfig.AccessClass</Name><Value>4</Value></ParameterValueStruct>"+opcionais+"</ParameterList><ParameterKey></ParameterKey></cwmp:SetParameterValues></soap:Body></soap:Envelope>");
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

        public void OnRequestBloqueio(object sender, RequestEventArgs e)
        {
            // Write info to the buffer.
            e.Response.Connection.Type = ConnectionType.Close;
            if (GlobalVar.UnlockSent == 0)
            {
                string opcionais = "";
                int numparams = 1;
                if (checkBox1.Checked)
                {
                    opcionais += "<ParameterValueStruct><Name>InternetGatewayDevice.Services.X_Pace_Com.Services.SSH.Enable</Name><Value>1</Value></ParameterValueStruct>";
                    numparams++;
                }
                else
                {
                    opcionais += "<ParameterValueStruct><Name>InternetGatewayDevice.Services.X_Pace_Com.Services.SSH.Enable</Name><Value>0</Value></ParameterValueStruct>";
                    numparams++;
                }

                AppendTextBox("Modem conectado, enviando configuração.\r\n");
                byte[] buffer = Encoding.Default.GetBytes("<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:cwmp=\"urn:dslforum-org:cwmp-1-0\"><soap:Header><cwmp:NoMoreRequests>0</cwmp:NoMoreRequests></soap:Header><soap:Body><cwmp:SetParameterValues><ParameterList encodingStyle:arrayType=\"cwmp:ParameterValueStruct["+numparams+"]\"><ParameterValueStruct><Name>InternetGatewayDevice.Services.X_Pace_Com.Services.GvtConfig.AccessClass</Name><Value>2</Value></ParameterValueStruct>" + opcionais + "</ParameterList><ParameterKey></ParameterKey></cwmp:SetParameterValues></soap:Body></soap:Envelope>");
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

        private void button2_Click(object sender, EventArgs e)
        {
            var certificate = new X509Certificate2("cert.p12", "");
            var listener = (SecureHttpListener)HttpListener.Create(IPAddress.Any, 443, certificate);

            GlobalVar.UnlockSent = 0;
            listener.UseClientCertificate = true;
            listener.RequestReceived += OnRequestDesbloqueio;
            listener.Start(5);

            AppendTextBox("Servidor web iniciado.\r\n");

            webBrowser1.Document.GetElementById("login").SetAttribute("value", "admin");
            webBrowser1.Document.GetElementById("password").SetAttribute("value", "gvt12345");
            webBrowser1.Document.InvokeScript("checkSubmit");
            HtmlElement form = webBrowser1.Document.GetElementById("login_form");
            if (form != null)
                form.InvokeMember("submit");
            webBrowser1.Navigated += webBrowser1_Navigated_1;
        }

        void webBrowser1_Navigated_1(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (webBrowser1.Url.ToString() == "http://192.168.25.1/pt_BR/admin/config_lan.htm")
            {
                string strSource = webBrowser1.DocumentText;
                string strStart = "token: util.parseJson(\'\"";
                if (strSource.Contains(strStart))
                {
                    int Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                    var token = strSource.Substring(Start, 6);
                    AppendTextBox("Token encontrado: " + token + "\r\n");
                    AppendTextBox("Apontando host acs.gvt.com.br para 192.168.25.200\r\n");
                    webBrowser1.Navigate("http://192.168.25.1/cgi-bin/generic.cgi?token=" + token + "&write=LANDevice_1_HostConfig_StaticHost_1_IPAddress:192.168.25.200&write=LANDevice_1_HostConfig_StaticHost_1_Hostname:acs.gvt.com.br");
                    AppendTextBox("Reiniciando o modem para concluir o processo ...\r\n");
                    webBrowser1.Navigate("http://192.168.25.1/pt_BR/admin/resets.htm");
                }
                else
                {
                    MessageBox.Show("Token não encontrado, feche o programa e inicie o processo novamente.");
                }
            }
            else if (webBrowser1.Url.ToString() == "http://192.168.25.1/pt_BR/admin/resets.htm")
            {
                System.Threading.Thread.Sleep(5000);
                string strSource = webBrowser1.DocumentText;
                string strStart = "token: util.parseJson(\'\"";
                if (strSource.Contains(strStart))
                {
                    int Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                    var token = strSource.Substring(Start, 6);
                    AppendTextBox("Token encontrado: " + token + "\r\n");
                    webBrowser1.Navigate("http://192.168.25.1/cgi-bin/generic.cgi?token=" + token + "&fct=reboot");
                    AppendTextBox("Modem reiniciado. Aguarde ...");
                }
                else
                {
                    MessageBox.Show("Token não encontrado, reinicie o modem manualmente.");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var certificate = new X509Certificate2("cert.p12", "");
            var listener = (SecureHttpListener)HttpListener.Create(IPAddress.Any, 443, certificate);

            GlobalVar.UnlockSent = 0;
            listener.UseClientCertificate = true;
            listener.RequestReceived += OnRequestBloqueio;
            listener.Start(5);

            AppendTextBox("Servidor web iniciado.\r\n");

            webBrowser1.Document.GetElementById("login").SetAttribute("value", "admin");
            webBrowser1.Document.GetElementById("password").SetAttribute("value", "gvt12345");
            webBrowser1.Document.InvokeScript("checkSubmit");
            HtmlElement form = webBrowser1.Document.GetElementById("login_form");
            if (form != null)
                form.InvokeMember("submit");
            webBrowser1.Navigated += webBrowser1_Navigated_1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

    public class CookieAwareWebClient : WebClient
    {
        public void Login(string loginPageAddress, NameValueCollection loginData)
        {
            CookieContainer container;

            var request = (HttpWebRequest)WebRequest.Create(loginPageAddress);

            request.Method = "POST";
            request.ContentType = "multipart/form-data";
            var buffer = Encoding.ASCII.GetBytes(loginData.ToString());
            request.ContentLength = buffer.Length;
            var requestStream = request.GetRequestStream();
            requestStream.Write(buffer, 0, buffer.Length);
            requestStream.Close();

            container = request.CookieContainer = new CookieContainer();

            var response = request.GetResponse();
            response.Close();
            CookieContainer = container;
        }

        public CookieAwareWebClient(CookieContainer container)
        {
            CookieContainer = container;
        }

        public CookieAwareWebClient()
            : this(new CookieContainer())
        { }

        public CookieContainer CookieContainer { get; private set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = (HttpWebRequest)base.GetWebRequest(address);
            request.CookieContainer = CookieContainer;
            return request;
        }
    }
 }
