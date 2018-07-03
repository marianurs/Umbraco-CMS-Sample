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
	/// <summary>Formularz reklamacji skarg i wniosków</summary>
	[PublishedContentModel("formularzReklamacjiSkargIWnioskow")]
	public partial class FormularzReklamacjiSkargIwnioskow : PublishedContentModel, ISeo, ISiteUrl
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "formularzReklamacjiSkargIWnioskow";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public FormularzReklamacjiSkargIwnioskow(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<FormularzReklamacjiSkargIwnioskow, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Tytuł strony
		///</summary>
		[ImplementPropertyType("sectionTitle")]
		public string SectionTitle
		{
			get { return this.GetPropertyValue<string>("sectionTitle"); }
		}

		///<summary>
		/// Meta description: Tekst zostanie dodany do meta description
		///</summary>
		[ImplementPropertyType("metaDescription")]
		public string MetaDescription
		{
			get { return Seo.GetMetaDescription(this); }
		}

		///<summary>
		/// Open Graph description: Tekst zostanie dodany do og:Description
		///</summary>
		[ImplementPropertyType("ogDescription")]
		public string OgDescription
		{
			get { return Seo.GetOgDescription(this); }
		}

		///<summary>
		/// Open Graph image: Ulr zostanie dodany do og:Image
		///</summary>
		[ImplementPropertyType("ogImage")]
		public string OgImage
		{
			get { return Seo.GetOgImage(this); }
		}

		///<summary>
		/// Open Graph title: Tekst zostanie dodany do og:Title
		///</summary>
		[ImplementPropertyType("ogTitle")]
		public string OgTitle
		{
			get { return Seo.GetOgTitle(this); }
		}

		///<summary>
		/// Page Title: Tytuł strony wyświetlany an pasku w przeglądarce
		///</summary>
		[ImplementPropertyType("pageTitle")]
		public string PageTitle
		{
			get { return Seo.GetPageTitle(this); }
		}

		///<summary>
		/// Alias strony
		///</summary>
		[ImplementPropertyType("umbracoUrlAlias")]
		public string UmbracoUrlAlias
		{
			get { return SiteUrl.GetUmbracoUrlAlias(this); }
		}

		///<summary>
		/// Url do strony: Adres url pod jakim będzie wyświetlana ta strona
		///</summary>
		[ImplementPropertyType("umbracoUrlName")]
		public string UmbracoUrlName
		{
			get { return SiteUrl.GetUmbracoUrlName(this); }
		}
	}
}