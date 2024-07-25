using UnityEngine;
using UnityEditor;
using System.Xml;
using System.Xml.Schema;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

//TODO: error checking for xml

public class XmlCardManager
{

	public static XmlSchema ParseCardXmlSchema(string file)
	{
		XmlSchema schema = null;
		try
		{
			XmlTextReader reader = new XmlTextReader(file);
			schema = XmlSchema.Read(reader, ValidationCallback);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
		}

		return schema;
	}

	static void ValidationCallback(object sender, ValidationEventArgs args)
	{
		if (args.Severity == XmlSeverityType.Warning)
			Debug.Log("WARNING: ");
		else if (args.Severity == XmlSeverityType.Error)
			Debug.Log("ERROR: ");

		Debug.Log(args.Message);
	}

	public static List<Card> ParseCardXml(string file)
	{
		XmlReader xmlFileReader;
		XmlReaderSettings settings = new XmlReaderSettings();
		/*XmlSchema schema = ParseCardXmlSchema("Assets/xml/cardSchema.xsd");

		settings.ValidationType = ValidationType.Schema;
		settings.Schemas.Add(schema);
		settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallback);*/

		List<Card> allCardList = new List<Card>();
		//xmlFileReader = XmlReader.Create(file, settings);
		xmlFileReader = XmlReader.Create(file);

		xmlFileReader.Read();
		while (xmlFileReader.Read())
		{
			xmlFileReader.MoveToElement();

			if (xmlFileReader.EOF)
			{
				break;
			}

			if(xmlFileReader.Name.Equals("Card"))
			{
				Card newCard = new Card();

				if (xmlFileReader.HasAttributes)
				{
					int i;
					for (i = 0; i < xmlFileReader.AttributeCount; i++)
					{
						xmlFileReader.MoveToAttribute(i);
						if (xmlFileReader.Name.Equals("name"))
						{
							newCard.cardName = xmlFileReader.ReadContentAsString();
						}
					}
				}

				/* Enter sub-loop for gathering card info */
				while(xmlFileReader.Read())
				{
					/* Move to element name */
					/* Or, break whole loop when encountering end element */
					while (xmlFileReader.NodeType != XmlNodeType.Element)
					{
						if (xmlFileReader.NodeType == XmlNodeType.EndElement)
						{
							break;
						}
						/* Otherwise, continue finding the current element */
						xmlFileReader.Read();
					}
					/* Either found the end of the card, or something unexpected happened. Either way, quit. */
					if (xmlFileReader.NodeType == XmlNodeType.EndElement)
					{
						break;
					}

					/* Parsing through card data */
					string tempFieldName = xmlFileReader.Name;

					/* Move to value */
					xmlFileReader.Read();

					switch (tempFieldName)
					{
						case "cardText":
							newCard.cardText = xmlFileReader.ReadContentAsString();
							break;
						case "consume":
							newCard.cost = (short)xmlFileReader.ReadContentAsInt();
							break;
						case "flavorText":
							newCard.flavorText = xmlFileReader.ReadContentAsString();
							break;
						case "cost":
							newCard.cost = (short)xmlFileReader.ReadContentAsInt();
							break;
						case "id":
							newCard.id = xmlFileReader.ReadContentAsString();
							break;
						case "types":
							newCard.typesList = new string[Card.MAX_TYPES];
							int j = 0;
							while (xmlFileReader.Read())
							{
								if (xmlFileReader.Name.Equals("types") && xmlFileReader.NodeType == XmlNodeType.EndElement)
								{
									break;
								}
								else if (xmlFileReader.NodeType == XmlNodeType.Text)
								{
									newCard.typesList[j] = xmlFileReader.ReadContentAsString();
									j++;
								}
							}
							break;
						default:
							/* This ensures not to enter an infinite loop scenario */
							xmlFileReader.ReadContentAsString();
							break;
					}

					/* Move to end element */
					while (xmlFileReader.NodeType != XmlNodeType.EndElement)
					{
						xmlFileReader.Read();
						xmlFileReader.MoveToElement();
					}
				}

				allCardList.Add(newCard);
			}
			/* Enter sub-loop for gathering deck info */
			else if (xmlFileReader.Name.Equals("DeckInfo"))
			{
			}

		}

		return allCardList;
	}

	/*
	 * Parses all xml files in the assets\xml folder. 
	 */
	static void ParseAllCardXml()
	{

	}
}