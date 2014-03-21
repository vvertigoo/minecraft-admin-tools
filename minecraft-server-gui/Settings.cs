using System.Collections.Generic;

namespace minecraft_server_gui.Properties {
    
    
    // Этот класс позволяет обрабатывать определенные события в классе параметров:
    //  Событие SettingChanging возникает перед изменением значения параметра.
    //  Событие PropertyChanged возникает после изменения значения параметра.
    //  Событие SettingsLoaded возникает после загрузки значений параметров.
    //  Событие SettingsSaving возникает перед сохранением значений параметров.
    internal sealed partial class Settings {

        public UpdateMacrosNames UpdateMacrosNamesMethod;
        public bool IsAdminWindowActive;
        public bool IsAdminSettingsWindowActive;
        public bool IsPlayerWindowActive;
        public bool IsSettingsWindowActive;
        public List<string> PlayerNames;
        
        public Settings() {
            // // Для добавления обработчиков событий для сохранения и изменения параметров раскомментируйте приведенные ниже строки:
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            this.SettingsSaving += this.SettingsSavingEventHandler;
            IsAdminWindowActive = false;
            IsAdminSettingsWindowActive = false;
            IsPlayerWindowActive = false;
            IsSettingsWindowActive = false;
            PlayerNames = new List<string>();
            //
        }
        
        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) {
            // Добавьте здесь код для обработки события SettingChangingEvent.
        }
        
        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            if (IsAdminWindowActive)
            {
                UpdateMacrosNamesMethod();
            }
        }
    }
}
