# Expense Claim API

## Overview
This repository contains a .NET 6 Web API that processes incoming text containing embedded XML and tagged fields to extract relevant expense claim details. The API validates the data, calculates sales tax, and handles specific failure conditions

## Features
- Accepts a block of text via a POST request.
- Extracts `<total>`, `<cost_centre>`, `<payment_method>`, and other relevant XML fields.
- Calculates total excluding tax and sales tax based on the extracted `<total>`.
- Implements validation for:
  - Corresponding opening and closing tags.
  - Presence of `<total>` and `<cost_centre>`.
  - Defaults `<cost_centre>` to 'UNKNOWN' if missing.

## Failure Conditions
- Rejects messages with unmatched tags.
- Rejects messages without a `<total>`.
- Defaults missing `<cost_centre>` to 'UNKNOWN'.

## Tech Stack
- .NET 6
- C#
- Regular Expressions for XML extraction
- xUnit for unit testing
-Dependency Injection to support a modular architecture and improve maintainability


 
