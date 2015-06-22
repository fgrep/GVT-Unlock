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
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using HttpServer;
using HttpServer.Headers;
using HttpListener = HttpServer.HttpListener;

namespace GVT_Unlock
{
    public partial class Form1 : Form
    {
        int tempoEspera = 0;
        int unlockSent = 0;
        bool requestLogin = false;
        bool requestReboot = false;
        bool desbRemoto = false;
        string configToken = "";
        string rebootToken = "";
        string ipModem = "192.168.25.1";
        string ipGerencia = "192.168.25.200";
        string servidorRemoto = "desbloqueiogvt.ddns.net";
        string servidorRemotoIP = "";

        public Form1()
        {
            InitializeComponent();
            ((Control)webBrowser1).Enabled = false;
        }

        public void AppendTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }
            textBox1.AppendText(value);
        }

        public void OnRequestDesbloqueio(object sender, RequestEventArgs e)
        {
            e.Response.Connection.Type = ConnectionType.Close;
            e.Response.ContentType.Value = "text/xml";
            if (unlockSent == 0)
            {
                unlockSent++;

                string opcionais = "";
                int numparams = 1;
                if (checkBox1.Checked)
                {
                    opcionais += "<ParameterValueStruct><Name>InternetGatewayDevice.Services.X_Pace_Com.Services.SSH.Enable</Name><Value xsi:type=\"xsd:unsignedInt\">1</Value></ParameterValueStruct>";
                    numparams++;
                }

                timer1.Stop();
                AppendTextBox("\r\nModem conectado, enviando configuração.\r\n");
                byte[] buffer = Encoding.Default.GetBytes("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<soap-env:Envelope xmlns:soap-enc=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:soap-env=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:cwmp=\"urn:dslforum-org:cwmp-1-0\"><soap-env:Header><cwmp:ID soap-env:mustUnderstand=\"1\">55882f2177ab936c3f062f8f</cwmp:ID></soap-env:Header><soap-env:Body><cwmp:SetParameterValues><ParameterList soap-enc:arrayType=\"cwmp:ParameterValueStruct["+numparams+"]\"><ParameterValueStruct><Name>InternetGatewayDevice.Services.X_Pace_Com.Services.GvtConfig.AccessClass</Name><Value xsi:type=\"xsd:unsignedInt\">4</Value></ParameterValueStruct>"+opcionais+"</ParameterList><ParameterKey/></cwmp:SetParameterValues></soap-env:Body></soap-env:Envelope>");
                e.Response.Body.Write(buffer, 0, buffer.Length);
                AppendTextBox("Finalizado.\r\n");
            }
            else if (unlockSent < 2)
            {
                unlockSent++;
                byte[] buffer = Encoding.Default.GetBytes("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<soap-env:Envelope xmlns:soap-enc=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:soap-env=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:cwmp=\"urn:dslforum-org:cwmp-1-0\"><soap-env:Header><cwmp:ID soap-env:mustUnderstand=\"1\">null</cwmp:ID></soap-env:Header><soap-env:Body><cwmp:InformResponse><MaxEnvelopes>1</MaxEnvelopes></cwmp:InformResponse></soap-env:Body></soap-env:Envelope>");
                e.Response.Body.Write(buffer, 0, buffer.Length);
            }
            else
            {
                unlockSent = 1;
                e.Response.Status = HttpStatusCode.NoContent;
            }
        }
        /*
        public void OnRequestBloqueio(object sender, RequestEventArgs e)
        {
            e.Response.Connection.Type = ConnectionType.Close;
            if (unlockSent == 0)
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

                timer1.Stop();
                AppendTextBox("\r\nModem conectado, enviando configuração.\r\n");
                byte[] buffer = Encoding.Default.GetBytes("<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:cwmp=\"urn:dslforum-org:cwmp-1-0\"><soap:Header><cwmp:NoMoreRequests>0</cwmp:NoMoreRequests></soap:Header><soap:Body><cwmp:SetParameterValues><ParameterList encodingStyle:arrayType=\"cwmp:ParameterValueStruct["+numparams+"]\"><ParameterValueStruct><Name>InternetGatewayDevice.Services.X_Pace_Com.Services.GvtConfig.AccessClass</Name><Value>2</Value></ParameterValueStruct>" + opcionais + "</ParameterList><ParameterKey></ParameterKey></cwmp:SetParameterValues></soap:Body></soap:Envelope>");
                e.Response.Body.Write(buffer, 0, buffer.Length);
                unlockSent = 1;
            }
            else if (unlockSent == 1)
            {
                byte[] buffer = Encoding.Default.GetBytes("<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"               xmlns:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\"               xmlns:cwmp=\"urn:dslforum-org:cwmp-1-0\">  <soap:Header>    <cwmp:NoMoreRequests>0</cwmp:NoMoreRequests>  </soap:Header>  <soap:Body>    <cwmp:InformResponse>      <MaxEnvelopes>1</MaxEnvelopes>    </cwmp:InformResponse>  </soap:Body></soap:Envelope>");
                e.Response.Body.Write(buffer, 0, buffer.Length);
                unlockSent = 2;
                AppendTextBox("Finalizado.\r\n");
            }
        }
        */
        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            AppendTextBox("Efetuando login no modem ... ");
            requestLogin = true;
            webBrowser1.Navigate("http://"+ipModem+"/pt_BR/admin/config_lan.htm");
        }

        private void btnDesbLocal_Click(object sender, EventArgs e)
        {
            btnDesbLocal.Enabled = false;
            btnDesbRemoto.Enabled = false;

            var certificate = new X509Certificate2("cert.p12", "acs.gvt.com.br");
            var listener = (SecureHttpListener)HttpListener.Create(IPAddress.Any, 443, certificate);

            unlockSent = 0;
            listener.UseClientCertificate = true;
            listener.RequestReceived += OnRequestDesbloqueio;
            listener.Start(5);
            AppendTextBox("Desbloqueio local. Servidor web iniciado.\r\n");

            if (configToken.Length == 6)
            {
                checkBox1.Enabled = false;
                AppendTextBox("Apontando host acs.gvt.com.br para "+ipGerencia+"\r\n");
                webBrowser1.Navigate("http://"+ipModem+"/cgi-bin/generic.cgi?token=" + configToken + "&write=LANDevice_1_HostConfig_StaticHost_1_IPAddress:"+ipGerencia+"&write=LANDevice_1_HostConfig_StaticHost_1_Hostname:acs.gvt.com.br");
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.Url.ToString() == "http://"+ipModem+"/pt_BR/admin/config_lan.htm")
            {
                if (requestLogin == true)
                {
                    HtmlElement form = webBrowser1.Document.GetElementById("login_form");
                    if (form != null)
                    {
                        webBrowser1.Document.GetElementById("login").SetAttribute("value", "admin");
                        webBrowser1.Document.GetElementById("password").SetAttribute("value", "gvt12345");
                        webBrowser1.Document.InvokeScript("checkSubmit");
                        form.InvokeMember("submit");
                        requestLogin = false;
                    }
                    else
                    {
                        AppendTextBox("Erro.\r\nPágina de login não encontrada. Tente novamente ou reinicie o programa\r\n");
                    }
                }
                else
                {
                    string strSource = webBrowser1.DocumentText;
                    string strStart = "token: util.parseJson(\'\"";
                    if (strSource.Contains(strStart))
                    {
                        int Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                        configToken = strSource.Substring(Start, 6);
                        if (configToken.Length == 6)
                        {
                            AppendTextBox("OK\r\n");
                            AppendTextBox("Token encontrado: " + configToken + "\r\n");
                            AppendTextBox("Pronto para iniciar desbloqueio.\r\n");
                            btnLogin.Enabled = false;
                            btnDesbLocal.Enabled = true;

                            if (lblServerStatus.Text == "Online")
                                btnDesbRemoto.Enabled = true;
                        }
                        else
                        {
                            AppendTextBox("Erro.\r\nToken não encontrado, reinicie o programa e tente novamente.");
                        }
                    }
                    else
                    {
                        AppendTextBox("Erro.\r\nToken não encontrado, reinicie o programa e tente novamente.");
                    }
                }
            }
            else if (webBrowser1.Url.ToString() == "http://"+ipModem+"/cgi-bin/generic.cgi?token=" + configToken + "&write=LANDevice_1_HostConfig_StaticHost_1_IPAddress:"+ipGerencia+"&write=LANDevice_1_HostConfig_StaticHost_1_Hostname:acs.gvt.com.br")
            {
                webBrowser1.Navigate("http://"+ipModem+"/cgi-bin/generic.cgi?token=" + configToken + "&read=LANDevice_1_HostConfig_StaticHost_1");

            }
            else if (webBrowser1.Url.ToString() == "http://" + ipModem + "/cgi-bin/generic.cgi?token=" + configToken + "&write=LANDevice_1_HostConfig_StaticHost_1_IPAddress:" + servidorRemotoIP + "&write=LANDevice_1_HostConfig_StaticHost_1_Hostname:acs.gvt.com.br")
            {
                webBrowser1.Navigate("http://" + ipModem + "/cgi-bin/generic.cgi?token=" + configToken + "&read=LANDevice_1_HostConfig_StaticHost_1");

            }
            else if (webBrowser1.Url.ToString() == "http://" + ipModem + "/cgi-bin/generic.cgi?token=" + configToken + "&read=LANDevice_1_HostConfig_StaticHost_1")
            {
                string strSource = webBrowser1.DocumentText;
                string strHost = "acs.gvt.com.br";
                string strIP = ipGerencia;

                if (desbRemoto == true)
                    strIP = servidorRemotoIP;

                if (strSource.Contains(strHost) && strSource.Contains(strIP))
                {
                    requestReboot = true;
                    AppendTextBox("Configuração OK. Reiniciando modem para finalizar o desbloqueio ...\r\n");
                    webBrowser1.Navigate("http://" + ipModem + "/pt_BR/admin/resets.htm");
                }
                else
                {
                    AppendTextBox("Erro na configuração. Tente novamente ou utilize o procedimento manual.\r\n");
                    btnDesbLocal.Enabled = true;
                    btnDesbRemoto.Enabled = true;
                }
            }
            else if (webBrowser1.Url.ToString() == "http://" + ipModem + "/pt_BR/admin/resets.htm" && requestReboot == true)
            {
                string strSource = webBrowser1.DocumentText;
                string strStart = "token: util.parseJson(\'\"";
                if (strSource.Contains(strStart))
                {
                    int Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                    rebootToken = strSource.Substring(Start, 6);
                    if (rebootToken.Length == 6)
                    {
                        AppendTextBox("Token encontrado: " + rebootToken + "\r\n");
                        webBrowser1.Navigate("http://"+ipModem+"/cgi-bin/generic.cgi?token=" + rebootToken + "&fct=reboot");
                    }
                    else
                    {
                        MessageBox.Show("Erro ao reiniciar modem. Reinicie o modem manualmente.");
                    }
                }
                else
                {
                    MessageBox.Show("Erro ao reiniciar modem. Reinicie o modem manualmente.");
                }
            }
            else if (webBrowser1.Url.ToString() == "http://" + ipModem + "/cgi-bin/generic.cgi?token=" + rebootToken + "&fct=reboot")
            {
                string strSource = webBrowser1.DocumentText;
                string strReboot = "reboot";
                if (strSource.Contains(strReboot))
                {
                    if (desbRemoto == true)
                    {
                        AppendTextBox("ATENÇÃO! Modem reiniciado.\r\n");
                        AppendTextBox("O desbloqueio remoto será efetuado quando seu modem se conectar a internet.\r\n");
                        AppendTextBox("Aguarde cerca de 10 minutos e verifique se o desbloqueio foi realizado.\r\n");
                        AppendTextBox("Você já pode fechar este programa.\r\n");
                    }
                    else
                    {
                        AppendTextBox("Modem reiniciado. Aguarde ");
                        timer1.Start();
                        label1.Visible = true;
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            AppendTextBox(".");
            tempoEspera++;

            TimeSpan ts = TimeSpan.FromSeconds(tempoEspera);
            label1.Text = String.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);

            if (tempoEspera == 360)
            {
                timer1.Stop();
                AppendTextBox("\r\nSeu modem está demorando para se conectar ao programa. Isso pode estar relacionado ao IP mal configurado, firewall/antivirus bloqueando o acesso do modem ao programa. Tente o desbloqueio remoto.\r\n");
            }
        }

        private void btnDesbRemoto_Click(object sender, EventArgs e)
        {
            btnDesbLocal.Enabled = false;
            btnDesbRemoto.Enabled = false;
            desbRemoto = true;

            AppendTextBox("Desbloqueio remoto.\r\n");

            if (configToken.Length == 6)
            {
                checkBox1.Enabled = false;
                AppendTextBox("Apontando host acs.gvt.com.br para " + servidorRemotoIP + "\r\n");
                webBrowser1.Navigate("http://" + ipModem + "/cgi-bin/generic.cgi?token=" + configToken + "&write=LANDevice_1_HostConfig_StaticHost_1_IPAddress:" + servidorRemotoIP + "&write=LANDevice_1_HostConfig_StaticHost_1_Hostname:acs.gvt.com.br");
            }

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            ChecarServidor();
        }

        private void ChecarServidor()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://"+servidorRemoto);
                request.Timeout = 3000;
                request.AllowAutoRedirect = false;
                request.Method = "HEAD";

                using (var response = request.GetResponse())
                {
                    IPAddress[] addresslist = Dns.GetHostAddresses(servidorRemoto);
                    servidorRemotoIP = addresslist[0].ToString();

                    lblServerStatus.Text = "Online";
                    lblServerStatus.ForeColor = System.Drawing.Color.Green;
                }
            }
            catch
            {
                lblServerStatus.Text = "Offline";
                lblServerStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
 }
