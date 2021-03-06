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
	// Mixin content Type 1210 with alias "selectMenuOrFooter"
	/// <summary>Menu/Footer</summary>
	public partial interface ISelectMenuOrFooter : IPublishedContent
	{
		/// <summary>Wybierz pozycję wyświetlenia sekcji</summary>
		nuPickers.Picker ChoiceMenuOrFooter { get; }
	}

	/// <summary>Menu/Footer</summary>
	[PublishedContentModel("selectMenuOrFooter")]
	public partial class SelectMenuOrFooter : PublishedContentModel, ISelectMenuOrFooter
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "selectMenuOrFooter";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public SelectMenuOrFooter(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<SelectMenuOrFooter, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Wybierz pozycję wyświetlenia sekcji
		///</summary>
		[ImplementPropertyType("choiceMenuOrFooter")]
		public nuPickers.Picker ChoiceMenuOrFooter
		{
			get { return GetChoiceMenuOrFooter(this); }
		}

		/// <summary>Static getter for Wybierz pozycję wyświetlenia sekcji</summary>
		public static nuPickers.Picker GetChoiceMenuOrFooter(ISelectMenuOrFooter that) { return that.GetPropertyValue<nuPickers.Picker>("choiceMenuOrFooter"); }
	}
}
