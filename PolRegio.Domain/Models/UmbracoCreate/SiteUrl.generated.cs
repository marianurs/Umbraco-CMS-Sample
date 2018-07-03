//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder v3.0.4.0
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.ModelsBuilder;
using Umbraco.ModelsBuilder.Umbraco;

namespace PolRegio.Domain.Models.UmbracoCreate
{
	// Mixin content Type 1314 with alias "siteUrl"
	/// <summary>Dodatkowe właściwiości</summary>
	public partial interface ISiteUrl : IPublishedContent
	{
		/// <summary>Alias strony</summary>
		string UmbracoUrlAlias { get; }

		/// <summary>Url do strony</summary>
		string UmbracoUrlName { get; }
	}

	/// <summary>Dodatkowe właściwiości</summary>
	[PublishedContentModel("siteUrl")]
	public partial class SiteUrl : PublishedContentModel, ISiteUrl
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "siteUrl";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public SiteUrl(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<SiteUrl, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Alias strony
		///</summary>
		[ImplementPropertyType("umbracoUrlAlias")]
		public string UmbracoUrlAlias
		{
			get { return GetUmbracoUrlAlias(this); }
		}

		/// <summary>Static getter for Alias strony</summary>
		public static string GetUmbracoUrlAlias(ISiteUrl that) { return that.GetPropertyValue<string>("umbracoUrlAlias"); }

		///<summary>
		/// Url do strony: Adres url pod jakim będzie wyświetlana ta strona
		///</summary>
		[ImplementPropertyType("umbracoUrlName")]
		public string UmbracoUrlName
		{
			get { return GetUmbracoUrlName(this); }
		}

		/// <summary>Static getter for Url do strony</summary>
		public static string GetUmbracoUrlName(ISiteUrl that) { return that.GetPropertyValue<string>("umbracoUrlName"); }
	}
}
