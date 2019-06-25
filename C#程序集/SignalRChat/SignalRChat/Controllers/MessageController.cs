using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SignalRChat.Controllers 
{
    [Route("[controller]/[action]")]
    public class MessageController : Controller
    {
        [HttpPost]
        public void Post(string user, string message)
        {
            WriteFile(user, message);
        }

        static void WriteFile(string user, string message)
        {
            // 获取json文件数据生成JArray对象
            var filePath = "./wwwroot/source/record.json";
            var fileStream = new FileStream(filePath, FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            string content = streamReader.ReadToEnd();
            streamReader.Close();
            fileStream.Close();
            JArray json = (JArray) JsonConvert.DeserializeObject(content);
            // 使用参数构建新的JObject
            string newJOS = "{\"name\":\""+user+"\",\"text\":\""+message+"\"}";
            JObject newJO = (JObject) JsonConvert.DeserializeObject(newJOS);
            // 添加到前面的JArray中
            json.Add(newJO);
            // 清空json文件
            fileStream = new FileStream(filePath, FileMode.Truncate, FileAccess.ReadWrite);
            fileStream.Close();
            // 重新写入新的内容
            fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            streamWriter.Write(json.ToString());
            streamWriter.Close();
            fileStream.Close();
        }
    }
}