using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SFB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utilities.Serialization
{
        public class Serializer
        {

        public static Serializer JSONSerializer;

            private JsonSerializer serializer;

            private JsonSerializerSettings settings;

            /// <summary>
            /// Constructor.
            /// </summary>
            public Serializer()
            {
                this.serializer = new JsonSerializer();
                this.serializer.Formatting = Formatting.Indented;
                this.serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                this.serializer.NullValueHandling = NullValueHandling.Include;

                this.settings = new JsonSerializerSettings();

                this.addConverter(new StringEnumConverter(true));

                foreach (JsonConverter converter in this.serializer.Converters)
                {
                    this.settings.Converters.Add(converter);
                }
                this.settings.Formatting = Formatting.Indented;
                this.settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                this.settings.NullValueHandling = NullValueHandling.Include;
            }

            /// <summary>
            /// Adds a new converter to the json serializer.
            /// </summary>
            /// <param name="converter">The type of json converter to add to the Serializer.</param>
            public void addConverter(JsonConverter converter)
            {
                this.serializer.Converters.Add(converter);
            }


            /// <summary>
            /// Deserializes an object from a .json file.
            /// </summary>
            /// <typeparam name="T">The type of object to deserialize into.</typeparam>
            /// <param name="p">The path to the file.</param>
            /// <returns>An object of specified type T.</returns>
            public T Deserialize<T>(string p)
            {
                string json = "";
                foreach (string line in File.ReadAllLines(p))
                {
                    json += line;
                }
                using (StreamReader sw = new StreamReader(p))
                using (JsonReader reader = new JsonTextReader(sw))
                {
                    var obj = this.serializer.Deserialize<T>(reader);
                    return obj;
                }
            }

            /// <summary>
            /// Deserializes an object from a .json file.
            /// </summary>
            /// <typeparam name="T">The type of object to deserialize into.</typeparam>
            /// <param name="p">The path to the file.</param>
            /// <returns>An object of specified type T.</returns>
            public object Deserialize(string p, Type T)
            {
                string json = "";
                foreach (string line in File.ReadAllLines(p))
                {
                    json += line;
                }
                using (StreamReader sw = new StreamReader(p))
                using (JsonReader reader = new JsonTextReader(sw))
                {
                    object obj = this.serializer.Deserialize(reader, T);
                    return obj;
                }
            }

            /// <summary>
            /// Serializes an object to a .json file.
            /// </summary>
            /// <param name="path"></param>
            /// <param name="o"></param>
            public void Serialize(string path, object o)
            {
                using (StreamWriter sw = new StreamWriter(path))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    this.serializer.Serialize(writer, o);
                }
            }



            /// <summary>
            /// Converts objects to json form.
            /// </summary>
            /// <param name="o"></param>
            /// <returns></returns>
            public string ToJSONString(object o)
            {
                return JsonConvert.SerializeObject(o, this.settings);
            }

            /// <summary>
            /// Converts from json form to objects.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="info"></param>
            /// <returns></returns>
            public T DeserializeFromJSONString<T>(string info)
            {
                return JsonConvert.DeserializeObject<T>(info, this.settings);
            }

            /// <summary>
            /// Converts from json form to objects.
            /// </summary>
            /// <param name="info"></param>
            /// <param name="T"></param>
            /// <returns></returns>
            public object DeserializeFromJSONString(string info, Type T)
            {
                return JsonConvert.DeserializeObject(info, T, this.settings);
            }

        /// <summary>
        /// Seralize the given object to a json file on the file system. Fails if no valid path is selected to save to.
        /// </summary>
        /// <param name="o"></param>
        public void Serialize(object o)
        {
            var path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "", "");

            if (String.IsNullOrEmpty(path))
            {
                return;
            }
            else
            {
                Serialize(path, o);
            }

        }

        /// <summary>
        /// Deserialize the selected .json file from the file system. Fails if a file is not selected.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Deserialize<T>()
        {
            var extensions = new[] {
            new ExtensionFilter("Content File", "json"),
            new ExtensionFilter("All Files", "*" ),
            };
            var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true);

            if (paths.Length == 0) return default(T);
            if (String.IsNullOrEmpty(paths[0]))
            {
                return default(T);
            }

            return Deserialize<T>(paths[0]);
        }

        /// <summary>
        /// Deserialize the selected .json file from the file system.
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        public object Deserialize(Type T)
        {
            var extensions = new[] {
            new ExtensionFilter("Content File", "json"),
            new ExtensionFilter("All Files", "*" ),
            };
            var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true);

            if (paths.Length == 0) return null;
            if (String.IsNullOrEmpty(paths[0]))
            {
                return null;
            }

            return Deserialize(paths[0],T);
        }

    }
}
