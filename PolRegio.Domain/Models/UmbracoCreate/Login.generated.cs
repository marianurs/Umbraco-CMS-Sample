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
	/// <summary>Logowanie</summary>
	[PublishedContentModel("login")]
	public partial class Login : PublishedContentModel, ISelectMenuOrFooter, ISiteUrl
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "login";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public Login(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<Login, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Wybierz pozycję wyświetlenia sekcji
		///</summary>
		[ImplementPropertyType("choiceMenuOrFooter")]
		public nuPickers.Picker ChoiceMenuOrFooter
		{
			get { return SelectMenuOrFooter.GetChoiceMenuOrFooter(this); }
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