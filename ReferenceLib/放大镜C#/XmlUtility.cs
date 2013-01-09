///----------------------------------------------------------------------------
/// Class     : XmlUtility
/// Purpose   : Simple XML serialization & desirialization suport. 
/// Written by: Ogun TIGLI
/// History   : 08 Jan 2005/Sat starting date.
///             
/// Notes: 
/// This software is provided 'as-is', without any express or implied 
/// warranty. In no event will the author be held liable for any damages 
/// arising from the use of this software.
/// 
/// Permission is granted to anyone to use this software for any purpose, 
/// including commercial applications, and to alter it and redistribute it 
/// freely, subject to the following restrictions:
///     1. The origin of this software must not be misrepresented; 
///        you must not claim that you wrote the original software. 
///        If you use this software in a product, an acknowledgment 
///        in the product documentation would be appreciated. 
///     2. Altered source versions must be plainly marked as such, and 
///        must not be misrepresented as being the original software.
///     3. This notice cannot be removed, changed or altered from any source 
///        code distribution.
/// 
///        (c) 2005-2007 Ogun TIGLI. All rights reserved. 
///----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace Magnifier20070401
{
    public class XmlUtility
    {
        public static void Serialize(Object data, string fileName)
        {
            Type type = data.GetType();
            XmlSerializer xs = new XmlSerializer(type);
            XmlTextWriter xmlWriter = new XmlTextWriter(fileName, System.Text.Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;
            xs.Serialize(xmlWriter, data);
            xmlWriter.Close();
        }

        public static Object Deserialize(Type type, string fileName)
        {
            XmlSerializer xs = new XmlSerializer(type);

            XmlTextReader xmlReader = new XmlTextReader(fileName);
            Object data = xs.Deserialize(xmlReader);

            xmlReader.Close();

            return data;
        }        
    } 
}
