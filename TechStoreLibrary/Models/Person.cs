using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStoreLibrary.Models.Base;

namespace TechStoreLibrary.Models
{
    /// <summary>
    /// Defines a person (worker or customer).
    /// </summary>
    public abstract class Person : BaseModel
    {
        #region Attributes
        private int id;
        private string firstName;
        private string lastName;
        private Address address;
        #endregion

        #region Properties
        /// <summary>
        /// Person's id.
        /// </summary>
        [Key]
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Person's first name.
        /// </summary>
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Person's last name.
        /// </summary>
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                OnPropertyChanged();
            }
        }

        public int AddressId { get; set; }

        /// <summary>
        /// Person's address.
        /// </summary>
        [ForeignKey("AddressId")]
        public Address Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructors
        public Person()
        {
        }

        public Person(string firstName, string lastName, Address address)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
        }
        #endregion

        #region Methods

        #endregion
    }
}
