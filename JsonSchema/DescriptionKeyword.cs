﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Json.Schema;

/// <summary>
/// Handles `description`.
/// </summary>
[SchemaKeyword(Name)]
[SchemaSpecVersion(SpecVersion.Draft6)]
[SchemaSpecVersion(SpecVersion.Draft7)]
[SchemaSpecVersion(SpecVersion.Draft201909)]
[SchemaSpecVersion(SpecVersion.Draft202012)]
[SchemaSpecVersion(SpecVersion.DraftNext)]
[Vocabulary(Vocabularies.Metadata201909Id)]
[Vocabulary(Vocabularies.Metadata202012Id)]
[Vocabulary(Vocabularies.MetadataNextId)]
[JsonConverter(typeof(DescriptionKeywordJsonConverter))]
public class DescriptionKeyword : IJsonSchemaKeyword
{
	/// <summary>
	/// The JSON name of the keyword.
	/// </summary>
	public const string Name = "description";

	/// <summary>
	/// The description.
	/// </summary>
	public string Value { get; }

	/// <summary>
	/// Creates a new <see cref="DescriptionKeyword"/>.
	/// </summary>
	/// <param name="value">The description.</param>
	public DescriptionKeyword(string value)
	{
		Value = value ?? throw new ArgumentNullException(nameof(value));
	}

	/// <summary>
	/// Builds a constraint object for a keyword.
	/// </summary>
	/// <param name="schemaConstraint">The <see cref="SchemaConstraint"/> for the schema object that houses this keyword.</param>
	/// <param name="localConstraints">
	/// The set of other <see cref="KeywordConstraint"/>s that have been processed prior to this one.
	/// Will contain the constraints for keyword dependencies.
	/// </param>
	/// <param name="context">The <see cref="EvaluationContext"/>.</param>
	/// <returns>A constraint object.</returns>
	public KeywordConstraint GetConstraint(SchemaConstraint schemaConstraint,
		IReadOnlyList<KeywordConstraint> localConstraints,
		EvaluationContext context)
	{
		return KeywordConstraint.SimpleAnnotation(Name, Value);
	}
}

/// <summary>
/// JSON converter for <see cref="DescriptionKeyword"/>.
/// </summary>
public sealed class DescriptionKeywordJsonConverter : JsonConverter<DescriptionKeyword>
{
	/// <summary>Reads and converts the JSON to type <see cref="DescriptionKeyword"/>.</summary>
	/// <param name="reader">The reader.</param>
	/// <param name="typeToConvert">The type to convert.</param>
	/// <param name="options">An object that specifies serialization options to use.</param>
	/// <returns>The converted value.</returns>
	public override DescriptionKeyword Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.String)
			throw new JsonException("Expected string");

		var str = reader.GetString()!;

		return new DescriptionKeyword(str);
	}

	/// <summary>Writes a specified value as JSON.</summary>
	/// <param name="writer">The writer to write to.</param>
	/// <param name="value">The value to convert to JSON.</param>
	/// <param name="options">An object that specifies serialization options to use.</param>
	public override void Write(Utf8JsonWriter writer, DescriptionKeyword value, JsonSerializerOptions options)
	{
		writer.WriteString(DescriptionKeyword.Name, value.Value);
	}
}