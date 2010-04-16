#region WatiN Copyright (C) 2006-2009 Jeroen van Menen

//Copyright 2006-2009 Jeroen van Menen
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

#endregion Copyright

using System;
using System.Runtime.Serialization;

namespace WatiN.Core.Exceptions
{
	/// <summary>
	/// Thrown if an element is readonly and the current action (like TextField.TypeText) a
	/// is not allowed.
	/// </summary>
    [Serializable]
    public class ElementReadOnlyException : ElementExceptionBase
	{
		public ElementReadOnlyException(Element element) :
            base("Element with Id:" + element.IdOrName + " is readonly") { Element = element; }

        public ElementReadOnlyException(SerializationInfo info, StreamingContext context) : base(info, context) {}
	}
}