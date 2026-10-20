# Uuid

[![CI](https://github.com/OWNER/REPO/actions/workflows/ci.yml/badge.svg)](https://github.com/OWNER/REPO/actions/workflows/ci.yml)

Replace `OWNER/REPO` in the badge URL with this repository’s GitHub path.

## Problem

Given a string of 32-bit integers separated by single spaces, print a longest strictly increasing subsequence of those values. The console writes one line:

```text
the sequence is <values>
```

Empty input yields an empty sequence. Invalid tokens produce a message on stderr and a non-zero exit code.

## Assumptions

- **Subsequence (contiguous).** Chosen values must occupy consecutive positions in the input. Skipping elements is not allowed. Equal adjacent values break a run.
- **Strictly increasing.** Each next value must be greater than the previous (`>`), not greater-or-equal.
- **Tie-break.** If several runs have the same maximum length, the **earliest** one is used: the run with the smallest start index. For contiguous runs that is the same as the lexicographically smallest index tuple.

Example: `1 3 2 4` has two runs of length 2 (`1 3` and `2 4`). The result is `1 3`.

## Build, run, and test

Requires the .NET 10 SDK.

```powershell
dotnet restore
dotnet build -c Release
dotnet test
```

Interactive console (prompts for the next line until you enter a blank line):

```powershell
dotnet run --project src/Uuid.Console
```

One-shot, with the numbers as arguments:

```powershell
dotnet run --project src/Uuid.Console -- 1 3 2 4
```

## Docker

Build the console image (tests are skipped unless you target the test stage):

```powershell
docker build -t uuid-console .
```

Optional: run tests as part of the image build:

```powershell
docker build --target test -t uuid-tests .
```

Run with the input as an argument:

```powershell
docker run --rm uuid-console 1 3 2 4
docker run --rm uuid-console "9 20000 1710 2461 9288 10195 10431 12485 12292"
```

## Coverage

Collect Cobertura coverage for `Uuid.Core`:

```powershell
dotnet test Uuid.slnx -c Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings --results-directory coverage
```

The XML lands under `coverage/<id>/coverage.cobertura.xml`.

HTML report (after installing the ReportGenerator global tool):

```powershell
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:coverage/**/coverage.cobertura.xml -targetdir:coveragereport -reporttypes:Html
```

Open `coveragereport/index.html`. CI also generates this report, writes a summary on the workflow page, and uploads the **coverage-report** artifact.

## Design

Work is split by responsibility in `Uuid.Core`:

| Type | Role |
| --- | --- |
| `IntegerSequenceParser` | Split on a single space and parse `int` values |
| `LongestStrictlyIncreasingSubsequenceFinder` | Scan for the longest contiguous strictly increasing run |
| `SubsequenceFormatter` | Join values with single spaces |
| `LongestIncreasingSubsequenceService` | Parse → find → format |

`Uuid.Console` only reads arguments or stdin, calls `Solve`, and prints the result. Invalid input is `FormatException` or `OverflowException`. The test project covers the main success and failure paths.

Analyzers, code-style checks, and warnings-as-errors run on build. CI also runs `dotnet format --verify-no-changes`.

## Complexity

Let `n` be the number of integers and `k` the length of the chosen run.

- **Time:** `O(n)` — one forward pass to track the current run and the best run so far.
- **Space:** `O(k)` for the returned values and indices (`O(1)` extra besides that output).
