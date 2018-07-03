using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace PolRegio.Domain.Models.Config
{
    /// <summary>
    /// Klasa zawierająca elementy BaseConfigurationCollection 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseConfigurationCollection<T> : ConfigurationElementCollection, IEnumerable<T> where T : ConfigurationElement, new()
    {
        /// <summary>
        /// BaseConfigurationCollection type
        /// </summary>
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }
        /// <summary>
        /// Dodaj nowy element
        /// </summary>
        public new string AddElementName
        {
            get
            {
                return base.AddElementName;
            }

            set
            {
                base.AddElementName = value;
            }

        }
        /// <summary>
        /// Wyczyść element
        /// </summary>
        public new string ClearElementName
        {
            get
            {
                return base.ClearElementName;
            }

            set
            {
                base.ClearElementName = value;
            }

        }
        /// <summary>
        /// Remove Element Name
        /// </summary>
        public new string RemoveElementName
        {
            get
            {
                return base.RemoveElementName;
            }
        }
        /// <summary>
        /// Ilość elementów
        /// </summary>
        public new int Count
        {
            get
            {
                return base.Count;
            }
        }
        /// <summary>
        /// Metoda usuwająca element z listy
        /// </summary>
        /// <param name="index">element index</param>
        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }
        /// <summary>
        /// Metoda usuwająca element z listy
        /// </summary>
        /// <param name="name">element name</param>
        public void Remove(string name)
        {
            BaseRemove(name);
        }
        /// <summary>
        /// Metoda czyszcząca listę
        /// </summary>
        public void Clear()
        {
            BaseClear();
        }
        /// <summary>
        /// Zwraca enumerator
        /// </summary>
        /// <returns></returns>
        public new IEnumerator<T> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }
        /// <summary>
        /// BaseConfigurationCollection element
        /// </summary>
        /// <param name="index">element index</param>
        /// <returns>T object type</returns>
        public T this[int index]
        {
            get
            {
                return (T)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }
        /// <summary>
        /// BaseConfigurationCollection element
        /// </summary>
        /// <param name="Value">element value</param>
        /// <returns>T object type</returns>
        new public T this[string Value]
        {
            get
            {
                return (T)BaseGet(Value);
            }
        }
        /// <summary>
        /// Index elementu
        /// </summary>
        /// <param name="value">T element</param>
        /// <returns>index of T element</returns>
        public int IndexOf(T value)
        {
            return BaseIndexOf(value);
        }
        /// <summary>
        /// Metoda dodająca nowy element do kolekcji
        /// </summary>
        /// <param name="value">T object value</param>
        public void Add(T value)
        {
            BaseAdd(value);
        }
        /// <summary>
        /// Protected list of T elementów
        /// </summary>
        protected List<T> _elements = new List<T>();
        /// <summary>
        /// Metoda dodająca nowy element
        /// </summary>
        /// <returns>ConfigurationElement</returns>
        protected override
            ConfigurationElement CreateNewElement()
        {
            T newElement = new T();
            _elements.Add(newElement);
            return newElement;
        }
        /// <summary>
        /// Metoda dodająca nowy element
        /// </summary>
        /// <param name="elementName">element name</param>
        /// <returns>ConfigurationElement</returns>
        protected override
            ConfigurationElement CreateNewElement(
            string elementName)
        {
            return new T();
        }
        /// <summary>
        /// Metoda zwracająca klucz elementu
        /// </summary>
        /// <param name="element">ConfigurationElement</param>
        /// <returns>object</returns>
        protected override Object
            GetElementKey(ConfigurationElement element)
        {
            return _elements.Find(e => e.Equals(element));
        }
        /// <summary>
        /// Base Add
        /// </summary>
        /// <param name="element">ConfigurationElement</param>
        protected override void
            BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

    }
}
