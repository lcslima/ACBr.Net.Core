// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 07-30-2016
//
// Last Modified By : RFTD
// Last Modified On : 07-30-2016
// ***********************************************************************
// <copyright file="XDocumentExtensions.cs" company="ACBr.Net">
//		        		   The MIT License (MIT)
//	     		    Copyright (c) 2016 Grupo ACBr.Net
//
//	 Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//	 The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//	 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ACBr.Net.Core.Extensions
{
	public static class XDocumentExtensions
	{
		public static string AsString(this XDocument xmlDoc, bool identado = false, bool showDeclaration = true)
		{
			return xmlDoc.AsString(identado, showDeclaration, Encoding.UTF8);
		}

		public static string AsString(this XDocument xmlDoc, bool identado, bool showDeclaration, Encoding encode)
		{
			using (var stringWriter = new StringWriter())
			{
				var settings = new XmlWriterSettings
				{
					Indent = identado,
					Encoding = encode,
					OmitXmlDeclaration = !showDeclaration
				};
				using (var xmlTextWriter = XmlWriter.Create(stringWriter, settings))
				{
					xmlDoc.WriteTo(xmlTextWriter);
					xmlTextWriter.Flush();
					return stringWriter.GetStringBuilder().ToString();
				}
			}
		}

		public static T GetValue<T>(this XElement element) where T : IConvertible
		{
			if (element == null) return default(T);

			T ret;
			try
			{
				ret = (T)Convert.ChangeType(element.Value, typeof(T));
			}
			catch (Exception)
			{
				ret = default(T);
			}

			return ret;
		}
	}
}