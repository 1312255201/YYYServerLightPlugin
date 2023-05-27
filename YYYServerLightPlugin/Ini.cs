using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PluginAPI.Core;
using Utils.NonAllocLINQ;

namespace YYYServerLightPlugin;

public class IniFile
{
    #region API函数声明
 
    [DllImport("kernel32")]//返回0表示失败，非0为成功
    private static extern long WritePrivateProfileString(string section, string key,
        string val, string filePath);
 
    [DllImport("kernel32")]//返回取得字符串缓冲区的长度
    private static extern long GetPrivateProfileString(string section, string key,
        string def, StringBuilder retVal, int size, string filePath);


    #endregion
    /// <summary>
    /// 读Ini文件
    /// </summary>
    /// <param name="Section">[]内的段落名</param>
    /// <param name="Key">key</param>
    /// <param name="NoText"></param>
    /// NoText对应API函数的def参数，它的值由用户指定，是当在配置文件中没有找到具体的Value时，就用NoText的值来代替。可以为空
    /// <param name="iniFilePath">ini配置文件的路径加ini文件名</param>
    /// <returns></returns>
    #region 读Ini文件
    private static string _url = "http://127.0.0.1:4579/";
    public static int ReadExp(string steam64id)
    {
        try
        {
            string path = "";
            path = "C:\\Users\\Administrator\\AppData\\Roaming\\SCP Secret Laboratory\\经验\\" + steam64id;

            if (File.Exists(path))
            {
                string[] conf = File.ReadAllLines(path);
                foreach (string cat in conf)
                {
                    if (cat.Contains("="))
                    {
                        if (cat.Split('=')[0] == steam64id)
                        {
                            string s = cat;
                            Regex r = new Regex(steam64id + "=");
                            try
                            {
                                Log.Info("读取成功" + int.Parse(r.Replace(s, "", 1)).ToString());
                                return int.Parse(r.Replace(s, "", 1));
                            }
                            catch
                            {
                                Log.Info("读取失败");
                                return -1;
                            }
                        }
                    }
                }
                return 0;
            }
            else
            {
                return -1;
            }
        }
        catch
        {
            return 0;
        }

    }
    public static int ReadLevel(int exp)
    {
        int Lv = 0;
        Lv = exp / 1000;
        return Lv;
    }
    public static string ReadLevel2(int exp)
    {
        int Lv = 0;
        string Lv2 = "";
        Lv = exp / 1000;
        if(Lv <=10)
        {
            Lv2 = Lv.ToString() + "|Neutralized";
        }
        if(Lv is > 10 and <= 30)
        {
            Lv2 = Lv.ToString() + "|Safe";

        }
        if (Lv is > 30 and <= 60 )
        {
            Lv2 = Lv.ToString() + "|Euclid";

        }
        if (Lv is > 60 and <= 100)
        {
            Lv2 = Lv.ToString() + "|Keter";
        }
        if (Lv is > 100 and <= 150)
        {
            Lv2 = Lv.ToString() + "|Thaumiel";
        }
        if (Lv > 150)
        {
            Lv2 = Lv.ToString() + "|O5";
        }
        return Lv2;
    }
    public static int MyExp(Player p)
    {
        Dictionary<string, string> param = new Dictionary<string, string>();
        param.Add("userid", p.UserId);
        string a = Get(_url + "myExp", param);
        if(a == "error")
        {
            return 0;
        }
        return(int.Parse(a));
    }
    public static void AddExp2(string p, int exp)
    {
        Dictionary<string, string> param = new Dictionary<string, string>();
        param.Add("userid", p);
        param.Add("nickname", Player.GetPlayers().FirstOrDefault(x=> x.UserId == p)?.Nickname);
        param.Add("exp", exp.ToString());
        new Task(() =>
        {
            Get(_url + "addExp", param);
        }).Start();
        var pp = Player.GetPlayers().FirstOrDefault(x => x.UserId == p);
        pp?.SendBroadcast("恭喜你获得了" + exp.ToString() + "点经验",5 );
    }

    public static void AddExp(string p, int exp)
    {
        var ppp = Player.GetPlayers().FirstOrDefault(pp => pp.UserId == p);
        Dictionary<string, string> param = new Dictionary<string, string>();
        param.Add("userid", p);
        param.Add("nickname", ppp.Nickname);
        param.Add("exp", exp.ToString());
        new Task(() =>
        {
            Get(_url + "addExp", param);
        }).Start();
       ppp.SendBroadcast( "恭喜你获得了" + exp.ToString() + "点经验",5);
    }
    public static string Get(string url, Dictionary<string, string> dic = null)
    {
        string result = "";
        if (dic == null)
        {
            dic = new Dictionary<string, string>();
        }
        StringBuilder builder = new StringBuilder();
        builder.Append(url);
        if (dic != null)
        {
            builder.Append("?");
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
        }

        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(builder.ToString());
        req.KeepAlive = false;
        //添加参数
        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
        Stream stream = resp.GetResponseStream();
        try
        {
            //获取内容
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }
        }
        finally
        {
            stream.Close();
        }
        return result;
    }
    #endregion
}