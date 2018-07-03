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
	/// <summary>Obsługa osób niepełnosprawnych</summary>
	[PublishedContentModel("disabilitiesSupport")]
	public partial class DisabilitiesSupport : PublishedContentModel, IAccordionWithAttachments, IArticleContent, ISeo, ISiteUrl
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "disabilitiesSupport";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public DisabilitiesSupport(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<DisabilitiesSupport, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Elementy akordeonu
		///</summary>
		[ImplementPropertyType("accordionElements")]
		public Archetype.Models.ArchetypeModel AccordionElements
		{
			get { return AccordionWithAttachments.GetAccordionElements(this); }
		}

		///<summary>
		/// Dodaj dokument: Dodaj dokument do pobrania
		///</summary>
		[ImplementPropertyType("addDocumentDown")]
		public Archetype.Models.ArchetypeModel AddDocumentDown
		{
			get { return ArticleContent.GetAddDocumentDown(this); }
		}

		///<summary>
		/// Dodaj zdjęcie: Dodaj zdjęcie do carousel
		///</summary>
		[ImplementPropertyType("articleImageList")]
		public Archetype.Models.ArchetypeModel ArticleImageList
		{
			get { return ArticleContent.GetArticleImageList(this); }
		}

		///<summary>
		/// Tag do artykulu: Dodaj tag do artykułu
		///</summary>
		[ImplementPropertyType("articleTag")]
		public object ArticleTag
		{
			get { return ArticleContent.GetArticleTag(this); }
		}

		///<summary>
		/// Tekst artykułu: Wprowadź treść artykułu
		///</summary>
		[ImplementPropertyType("articleText")]
		public IHtmlString ArticleText
		{
			get { return ArticleContent.GetArticleText(this); }
		}

		///<summary>
		/// Tytuł artykułu: Wprowadź tytuł artykułu
		///</summary>
		[ImplementPropertyType("articleTitle")]
		public string ArticleTitle
		{
			get { return ArticleContent.GetArticleTitle(this); }
		}

		///<summary>
		/// Zobacz wszystkie: Czy ma być pokazany link do strony ze wszystkimi artykułami
		///</summary>
		[ImplementPropertyType("seeAllArticles")]
		public bool SeeAllArticles
		{
			get { return ArticleContent.GetSeeAllArticles(this); }
		}

		///<summary>
		/// Social share: Czy mają być pokazane przyciski do share'owania kontentu
		///</summary>
		[ImplementPropertyType("socialShare")]
		public bool SocialShare
		{
			get { return ArticleContent.GetSocialShare(this); }
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