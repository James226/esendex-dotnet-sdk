﻿
namespace com.esendex.sdk.rest
{
    internal abstract class RestResource
    {
        public abstract string ResourceName { get; }

        public string Content { get; protected set; }

        public string ResourcePath { get; protected set; }

        public RestResource()
        {
            ResourcePath = ResourceName;
            Content = string.Empty;
        }

        public RestResource(string content)
            : this()
        {
            Content = content;
        }

        public override bool Equals(object obj)
        {
            RestResource other = obj as RestResource;

            if (other == null) return false;

            if (ResourcePath != other.ResourcePath) return false;
            if (ResourceName != other.ResourceName) return false;
            if (Content != other.Content) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
