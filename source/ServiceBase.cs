﻿using System;
using com.esendex.sdk.http;
using com.esendex.sdk.rest;
using com.esendex.sdk.utilities;

namespace com.esendex.sdk
{
    /// <summary>
    /// Represents a base service from which services are derived.
    /// </summary>
    public abstract class ServiceBase
    {
        internal ServiceBase(EsendexCredentials credentials)
        {
            IHttpRequestHelper httpRequestHelper = new HttpRequestHelper();
            IHttpResponseHelper httpResponseHelper = new HttpResponseHelper();

            IHttpClient httpClient = new HttpClient(credentials, Constants.API_URI, httpRequestHelper, httpResponseHelper);

            Serialiser = new XmlSerialiser();
            RestClient = new RestClient(httpClient);
        }

        internal ServiceBase(IRestClient restClient, ISerialiser serialiser)
        {
            Serialiser = serialiser;
            RestClient = restClient;
        }

        internal ISerialiser Serialiser { get; set; }
        internal IRestClient RestClient { get; set; }

        internal TResult MakeRequest<TResult>(HttpMethod method, RestResource resource) where TResult : class
        {
            RestResponse response = MakeRequest(method, resource);

            if (response == null) return null;

            return Serialiser.Deserialise<TResult>(response.Content);
        }

        internal RestResponse MakeRequest(HttpMethod method, RestResource resource)
        {
            switch (method)
            {
                case HttpMethod.POST:
                    return RestClient.Post(resource);
                case HttpMethod.PUT:
                    return RestClient.Put(resource);
                case HttpMethod.GET:
                    return RestClient.Get(resource);
                case HttpMethod.DELETE:
                    return RestClient.Delete(resource);
                default:
                    throw new ArgumentException(string.Format("An invalid HttpMethod was supplied for this type of resource."), "method");
            }
        }
    }
}