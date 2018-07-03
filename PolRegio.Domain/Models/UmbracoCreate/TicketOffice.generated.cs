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
	/// <summary>Kasa biletowa</summary>
	[PublishedContentModel("ticketOffice")]
	public partial class TicketOffice : PublishedContentModel, ISeo, ISiteUrl
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "ticketOffice";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public TicketOffice(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<TicketOffice, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Uwagi: Pole na dodatkowe uwagi co do kasy biletowej
		///</summary>
		[ImplementPropertyType("attention")]
		public IHtmlString Attention
		{
			get { return this.GetPropertyValue<IHtmlString>("attention"); }
		}

		///<summary>
		/// Płatność kartą
		///</summary>
		[ImplementPropertyType("cardPayment")]
		public bool CardPayment
		{
			get { return this.GetPropertyValue<bool>("cardPayment"); }
		}

		///<summary>
		/// Adres
		///</summary>
		[ImplementPropertyType("officeAddress")]
		public IHtmlString OfficeAddress
		{
			get { return this.GetPropertyValue<IHtmlString>("officeAddress"); }
		}

		///<summary>
		/// Lokalizacja
		///</summary>
		[ImplementPropertyType("officeLocalization")]
		public string OfficeLocalization
		{
			get { return this.GetPropertyValue<string>("officeLocalization"); }
		}

		///<summary>
		/// Godziny otwarcia
		///</summary>
		[ImplementPropertyType("openHours")]
		public IHtmlString OpenHours
		{
			get { return this.GetPropertyValue<IHtmlString>("openHours"); }
		}

		///<summary>
		/// Godziny otwarcia w soboty
		///</summary>
		[ImplementPropertyType("openHoursSaturday")]
		public IHtmlString OpenHoursSaturday
		{
			get { return this.GetPropertyValue<IHtmlString>("openHoursSaturday"); }
		}

		///<summary>
		/// Godziny otwarcia w niedzielę i święta
		///</summary>
		[ImplementPropertyType("openHoursSundayAndHolidays")]
		public IHtmlString OpenHoursSundayAndHolidays
		{
			get { return this.GetPropertyValue<IHtmlString>("openHoursSundayAndHolidays"); }
		}

		///<summary>
		/// Automat biletowy (24h)
		///</summary>
		[ImplementPropertyType("ticketMachine")]
		public bool TicketMachine
		{
			get { return this.GetPropertyValue<bool>("ticketMachine"); }
		}

		///<summary>
		/// Bilety na relacje międzynarodowe
		///</summary>
		[ImplementPropertyType("ticketsForInternationalRelations")]
		public bool TicketsForInternationalRelations
		{
			get { return this.GetPropertyValue<bool>("ticketsForInternationalRelations"); }
		}

		///<summary>
		/// Bilety na relacje krajowe
		///</summary>
		[ImplementPropertyType("ticketsForNationalRelations")]
		public bool TicketsForNationalRelations
		{
			get { return this.GetPropertyValue<bool>("ticketsForNationalRelations"); }
		}

		///<summary>
		/// Bilety z rezerwację miejsc
		///</summary>
		[ImplementPropertyType("ticketsWithSeatReservation")]
		public bool TicketsWithSeatReservation
		{
			get { return this.GetPropertyValue<bool>("ticketsWithSeatReservation"); }
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