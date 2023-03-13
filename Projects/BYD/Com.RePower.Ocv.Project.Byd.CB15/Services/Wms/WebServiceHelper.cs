using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Wms
{
    public static class WebServiceHelper
    {
        /// <summary>
        /// 静态缓存字典，速度提升至8倍
        /// </summary>
        private static Dictionary<string, object> _webServiceConfig = new Dictionary<string, object>();
        [Obsolete]
        //获取WSDL
        private static readonly WebClient wc = new WebClient();

        /// < summary>
        /// 动态调用web服务
        /// </summary>
        /// < param name="url">WSDL服务地址</param>
        /// < param name="classname">类名</param>
        /// < param name="methodname">方法名</param>
        /// < param name="args">参数</param>
        /// < returns></returns>
        [Obsolete]
        public static object InvokeWebService(this string url, string methodname, object[] args, string classname = "")
        {
            string key = $"{url}_{methodname}";//缓存Key唯一标识
            Type webService;
            if (!_webServiceConfig.ContainsKey(key))
            {
                string @namespace = "EnterpriseServerBase.WebService.DynamicWebCalling";
                //classname 一般自己指定 如果有些地址是http://www.webxml.com.cn/WebServices/WeatherWS?wsdl 就会调用失败
                if ((classname == null) || (classname == ""))
                {
                    classname = GetWsClassName(url);
                }
                try
                {
                    Stream stream = wc.OpenRead(url);
                    ServiceDescription sd = ServiceDescription.Read(stream);
                    ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
                    sdi.AddServiceDescription(sd, "", "");
                    CodeNamespace cn = new CodeNamespace(@namespace);
                    CodeCompileUnit ccu = new CodeCompileUnit();
                    ccu.Namespaces.Add(cn);
                    sdi.Import(cn, ccu);
                    CSharpCodeProvider icc = new CSharpCodeProvider();
                    CompilerParameters cplist = new CompilerParameters
                    {
                        GenerateExecutable = false,
                        GenerateInMemory = true
                    };
                    cplist.ReferencedAssemblies.Add("System.dll");
                    cplist.ReferencedAssemblies.Add("System.XML.dll");
                    cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                    cplist.ReferencedAssemblies.Add("System.Data.dll");
                    CompilerResults compiler = icc.CompileAssemblyFromDom(cplist, ccu);
                    icc.Dispose();
                    if (compiler.Errors.HasErrors)
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        foreach (CompilerError ce in compiler.Errors)
                        {
                            sb.Append(ce.ToString());
                            sb.Append(Environment.NewLine);
                        }
                        throw new Exception(sb.ToString());
                    }
                    System.Reflection.Assembly assembly = compiler.CompiledAssembly;
                    Type t = assembly.GetType(@namespace + "." + classname, true, true);
                    webService = t;
                    _webServiceConfig.Add(key, webService);//加入缓存
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                webService = _webServiceConfig[key] as Type;//获取缓存的数据
            }
            if (webService != null)
            {
                object obj = Activator.CreateInstance(webService);
                System.Reflection.MethodInfo mi = webService.GetMethod(methodname);
                if (mi != null)
                {
                    return mi.Invoke(obj, args);
                }
                throw new Exception($"找不到{methodname}这个方法，调用失败");
            }
            else
            {
                throw new Exception($"WebService 对象为空，调用失败");
            }
        }

        /// <summary>
        /// 获取wsdl类名称
        /// </summary>
        /// <param name="wsUrl">wsdl地址</param>
        /// <returns></returns>
        private static string GetWsClassName(string wsUrl)
        {
            string[] parts = wsUrl.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');//classname 一般自己指定 原因是因为在这里
            return pps[0];
        }
        /// <summary>
        /// 清空缓存
        /// </summary>
        public static void ClearCache()
        {
            _webServiceConfig.Clear();
        }
    }
}
