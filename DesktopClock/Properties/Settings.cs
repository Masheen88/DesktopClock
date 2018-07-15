﻿using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Media;
using Newtonsoft.Json;
using WpfWindowPlacement;

namespace DesktopClock.Properties
{
    public sealed class Settings : INotifyPropertyChanged
    {
        private static readonly Lazy<Settings> _default = new Lazy<Settings>(() => LoadOrCreate());
        private const string _path = "DesktopClock.settings";

        private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented
        };

        private Settings()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static Settings Default => _default.Value;

        #region "Properties"

        public WindowPlacement Placement { get; set; }
        public bool Topmost { get; set; } = true;
        public bool ShowInTaskbar { get; set; } = true;
        public int Height { get; set; } = 40;
        public string TimeZone { get; set; } = string.Empty;
        public string Format { get; set; } = "F";
        public Color Color { get; set; } = Colors.White;
        public double Opacity { get; set; } = 0.85;
        public Color TextColor { get; set; } = Colors.Black;
        public string FontFamily { get; set; } = "Consolas";

        #endregion "Properties"

        /// <summary>
        /// Save to the default path in JSON format.
        /// </summary>
        public void Save()
        {
            using (var fileStream = new FileStream(_path, FileMode.Create))
            using (var streamWriter = new StreamWriter(fileStream))
            using (var jsonWriter = new JsonTextWriter(streamWriter))
                JsonSerializer.Create(_jsonSerializerSettings).Serialize(jsonWriter, this);
        }

        /// <summary>
        /// Load from the default path in JSON format.
        /// </summary>
        private static Settings Load()
        {
            using (var fileStream = new FileStream(_path, FileMode.Open))
            using (var streamReader = new StreamReader(fileStream))
            using (var jsonReader = new JsonTextReader(streamReader))
                return JsonSerializer.Create(_jsonSerializerSettings).Deserialize<Settings>(jsonReader);
        }

        /// <summary>
        /// Load from the default path or return a new instance if it fails.
        /// </summary>
        private static Settings LoadOrCreate()
        {
            try
            {
                return Load();
            }
            catch
            {
                return new Settings();
            }
        }
    }
}