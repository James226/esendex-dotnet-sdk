﻿using System;
using com.esendex.sdk.http;
using com.esendex.sdk.rest;
using com.esendex.sdk.rest.resources;
using com.esendex.sdk.utilities;

namespace com.esendex.sdk.contacts
{
    /// <summary>
    /// Defines methods to manage contacts.
    /// </summary>
    public class ContactService : ServiceBase, IContactService
    {
        /// <summary>
        /// Initialises a new instance of the ContactService
        /// </summary>
        /// <param name="username">Your Esendex username.</param>
        /// <param name="password">Your Esendex password.</param>
        public ContactService(string username, string password)
            : this(new EsendexCredentials(username, password))
        {
        }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.contacts.ContactService
        /// </summary>
        /// <param name="credentials">A com.esendex.sdk.EsendexCredentials instance that contains access credentials.</param>
        public ContactService(EsendexCredentials credentials) 
            : base(credentials){ }

        internal ContactService(IRestClient restClient, ISerialiser serialiser)
            : base(restClient, serialiser) { }

        /// <summary>
        /// Creates a com.esendex.sdk.contacts.Contact instance and returns the new com.esendex.sdk.contacts.Contact instance.
        /// </summary>
        /// <param name="contact">A com.esendex.sdk.contacts.Contact instance that contains the contact.</param>
        /// <returns>A com.esendex.sdk.contacts.Contact instance that contains the contact with an Id assigned.</returns>        
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.Net.WebException"></exception>        
        public Contact CreateContact(Contact contact)
        {
            ContactCollection contacts = new ContactCollection(contact);

            string requestXml = Serialiser.Serialise<ContactCollection>(contacts);

            RestResource resource = new ContactsResource(requestXml);

            return MakeRequest<Contact>(HttpMethod.POST, resource);
        }

        /// <summary>
        /// Adds a com.esendex.sdk.contacts.ContactCollection instance and returns true if the contacts were added successfully; otherwise, false.
        /// </summary>
        /// <param name="contacts">A com.esendex.sdk.contacts.ContactCollection instance.</param>
        /// <returns>true, if the contacts were added successfully; otherwise, false.</returns>        
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.Net.WebException"></exception>
        public bool CreateContacts(ContactCollection contacts)
        {
            string requestXml = Serialiser.Serialise<ContactCollection>(contacts);

            RestResource resource = new ContactsResource(requestXml);

            RestResponse response = MakeRequest(HttpMethod.POST, resource);

            return (response != null);
        }

        /// <summary>
        /// Returns true if the contact was successfully deleted; otherwise, false.
        /// </summary>
        /// <param name="id">A System.Guid instance that contains the Id of a contact.</param>
        /// <returns>true, if the contact was successfully deleted; otherwise, false.</returns>        
        /// <exception cref="System.Net.WebException"></exception>
        public bool DeleteContact(Guid id)
        {
            RestResource resource = new ContactsResource(id);

            RestResponse response = MakeRequest(HttpMethod.DELETE, resource);

            return (response != null);
        }

        /// <summary>
        /// Returns true if the contact was successfully updated; otherwise, false.
        /// </summary>
        /// <param name="contact">A com.esendex.sdk.contacts.Contact instance that contains the contact.</param>
        /// <returns>true, if the contact was successfully updated; otherwise, false.</returns>        
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.Net.WebException"></exception>      
        public bool UpdateContact(Contact contact)
        {
            string requestXml = Serialiser.Serialise<Contact>(contact);

            RestResource resource = new ContactsResource(contact.Id, requestXml);

            RestResponse response = MakeRequest(HttpMethod.PUT, resource);

            return (response != null);
        }

        /// <summary>
        /// Gets a com.esendex.sdk.contact.Contact instance containing a contact.
        /// </summary>
        /// <param name="id">A System.Guid instance that contains the Id of a contact.</param>
        /// <returns>A com.esendex.sdk.contacts.Contact instance that contains the contact.</returns>        
        /// <exception cref="System.Net.WebException"></exception>
        public Contact GetContact(Guid id)
        {
            RestResource resource = new ContactsResource(id);

            return MakeRequest<Contact>(HttpMethod.GET, resource);
        }

        /// <summary>
        /// Gets a com.esendex.sdk.contact.PagedContactCollection instance containing contacts.
        /// </summary>
        /// <param name="pageNumber">The number of the page.</param>
        /// <param name="pageSize">The number of items in the page.</param>
        /// <returns>A com.esendex.sdk.contacts.PagedContactCollection instance that contains the contacts.</returns>        
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.Net.WebException"></exception>
        public PagedContactCollection GetContacts(int pageNumber, int pageSize)
        {
            RestResource resource = new ContactsResource(pageNumber, pageSize);

            return MakeRequest<PagedContactCollection>(HttpMethod.GET, resource);
        }
    }
}
