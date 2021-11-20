using System;

namespace Service.ImportExport
{
    public class PropertyByName<T>
    {
        private object _propertyValue;

        public PropertyByName(string propertyName)
        {
            PropertyName = propertyName;
            PropertyOrderPosition = 1;
        }

        public int PropertyOrderPosition { get; set; }

        public string PropertyName { get; }

        public object PropertyValue
        {
            get => _propertyValue;
            set => _propertyValue = value;
        }        
        
        public string StringValue => PropertyValue == null ? string.Empty : Convert.ToString(PropertyValue);

    }
}