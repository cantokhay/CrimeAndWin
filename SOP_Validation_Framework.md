# SOP: Standardized Validation Framework for CrimeAndWin

This document outlines the implementation and usage of the global validation framework across the **CrimeAndWin** microservices architecture using **FluentValidation** and **Mediator**.

## 1. Goal and Objectives
The goal is to ensure that all inputs (Commands, Queries, DTOs) and Domain Entities are validated automatically and consistently across all layers and microservices.

- **Automation**: Validation should trigger automatically before any business logic (Handler) executes.
- **Consistency**: Error responses should follow a standard JSON structure.
- **Precision**: Specialized rules for Game Mechanics (Number ranges, Date limits).

## 2. Architecture

### 2.1 Building Block: `Shared.Infrastructure`
Validation logic is centralized in the `000_BuildingBlocks\Shared.Infrastructure` project to avoid code duplication.
- **`ValidationBehavior<TRequest, TResponse>`**: A Mediator pipeline behavior that intercepts requests and throws a custom `ValidationException` if rules are violated.
- **`ValidationExtensions`**: Custom FluentValidation rules for `Date` and `Number` types.

### 2.2 Microservice Layering
- **Application Layer**: Contains the `ValidationRules` for every Request/Command.
- **Integration**: `Program.cs` registers all validators in the assembly and injects the pipeline behavior.

## 3. Implementation Steps

### Phase 1: Shared Core Setup
1. **Add Packages**: Install `FluentValidation.DependencyInjectionExtensions` and `Mediator.Abstractions` to `Shared.Infrastructure`.
2. **Create `ValidationBehavior`**: 
   - Intercepts `IRequest` (or `IMessage` if using Mediator.SourceGenerator).
   - Validates using `IEnumerable<IValidator<TRequest>>`.
   - Collects errors and throws `AppValidationException`.
3. **Common Rules**:
   - `Date`: `IsHistorical()`, `IsFuture()`, `WithinDays(n)`.
   - `Number`: `Positive()`, `InPercentageRange()`, `PositiveCurrency()`.

### Phase 2: Microservice Integration
1. **Registration**: 
   - Create an extension method `AddValidationServices` in `Shared.Infrastructure`.
   - This method will call `AddValidatorsFromAssembly` and `AddMediatorPipelineBehavior`.
2. **Global Exception Filter**:
   - Update `ApiExceptionFilter` to catch `AppValidationException` and return `400 Bad Request` with structured errors.

### Phase 3: Applying Validators
1. **Command Validators**: Every command (e.g., `CreateActionCommand`) MUST have a corresponding `CreateActionCommandValidator`.
2. **Entity Validators**: (Optional) Manual validation for Domain Entities when state transitions occur.

## 4. Coding Standards

### 4.1 Naming Conventions
- Validator file: `[ClassName]Validator.cs`
- Folder: `ValidationRules/[Domain]Validations/`

### 4.2 Example Rules (Numbers)
```csharp
RuleFor(x => x.Amount)
    .GreaterThan(0)
    .PrecisionScale(18, 2, false)
    .WithMessage("Miktar pozitif ve gecerli bir para birimi formatinda olmalidir.");
```

### 4.3 Example Rules (Dates)
```csharp
RuleFor(x => x.StartDate)
    .GreaterThanOrEqualTo(DateTime.UtcNow)
    .WithMessage("Baslangic tarihi gecmiste olamaz.");
```

## 5. Deployment and Verification
- **Test cases**: Create unit tests for validators (Testable via `ShouldHaveValidationErrorFor`).
- **Integration test**: Send invalid JSON to an endpoint and verify the `400` response with the correct error list.
