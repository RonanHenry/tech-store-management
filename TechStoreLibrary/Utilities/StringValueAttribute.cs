using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TechStoreLibrary.Utilities
{
    /// <summary>
    /// Defines an attribute to represent a string value in an Enum value.
    /// </summary>
    public class StringValueAttribute : Attribute
    {
        #region Attributes

        #endregion

        #region Properties
        /// <summary>
        /// Holds the string value for an Enum value.
        /// </summary>
        public string StringValue { get; protected set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor used to initialize a StringValue Attribute.
        /// </summary>
        /// <param name="value"></param>
        public StringValueAttribute(string value)
        {
            StringValue = value;
        }
        #endregion

        #region Methods

        #endregion
    }

    public static class EnumString
    {
        #region Methods
        /// <summary>
        /// Gets the string value of an Enum value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetStringValue(this Enum value)
        {
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());
            StringValueAttribute[] attributes = fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

            return attributes.Length > 0 ? attributes[0].StringValue : null;
        }
        #endregion
    }
}
