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
	/// <summary>Resetowanie Hasla</summary>
	[PublishedContentModel("resetPassword")]
	public partial class ResetPassword : PublishedContentModel, IHideableMenuItem
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "resetPassword";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public ResetPassword(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<ResetPassword, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// IsHidden
		///</summary>
		[ImplementPropertyType("isHidden")]
		public bool IsHidden
		{
			get { return HideableMenuItem.GetIsHidden(this); }
		}
	}
}