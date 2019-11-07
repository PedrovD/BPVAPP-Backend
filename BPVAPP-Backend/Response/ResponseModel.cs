using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BPVAPP_Backend.Response
{
    public class ResponseModel
    {
        public int StatusCode { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Message { get; set; }

        public JObject Data { get; set; }

        public ResponseModel(int status = 200, string message = "Success")
        {
            StatusCode = status;
            Message = message;
            TimeStamp = DateTime.UtcNow;
            Data = new JObject();
        }

        /// <summary>
        /// Adds a value in a json format
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Add(string name, object value)
        {
            if (value == null)
            {
                Data[name] = "null";
            }
            else
            {
                Data[name] = value.ToString();
            }
        }

        public void AddArrayIn(string objectName, string name, object[] data)
        {
            if (Data.ContainsKey(objectName))
            {
                if (Data[objectName] is JArray)
                {
                    var arr = JArray.FromObject(Data[objectName]);
                    var array = new JArray();
                    foreach (var item in data)
                    {
                        if (item is string)
                        {
                            array.Add(item);
                        }
                        else
                        {
                            array.Add(JObject.FromObject(item));
                        }
                    }
                    arr.Add(array);
                    Data[objectName] = arr;
                }
            }
        }

        public void AddObject(string name, object value)
        {
            if (value == null)
            {
                Data[name] = "null";
            }
            else
            {
                Data[name] = JObject.FromObject(value);
            }
        }

        /// <summary>
        /// Converts a array to json array
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public void AddArray(string name,object[] data)
        {
            var array = new JArray();

            foreach (var item in data)
            {
                if (item is string)
                {
                    array.Add(item);
                }
                else
                {
                    array.Add(JObject.FromObject(item));
                }
            }
            Data[name] = array;
        }

        /// <summary>
        /// Converts a list to json array
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public void AddList(string name, IEnumerable<object> data)
        {
            var array = new JArray();

            foreach (var item in data)
            {
                if (item is string)
                {
                    array.Add(item);
                }
                else
                {
                    array.Add(JObject.FromObject(item));
                }
            }
            Data[name] = array;
        }
    }
}
