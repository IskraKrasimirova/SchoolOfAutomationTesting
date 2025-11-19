# SimpleCalculator

This is a console-based calculator written in C#. It supports basic and advanced arithmetic operations. The project is organized into separate classes for better readability and maintainability.

## Supported Operations

The calculator allows the following operations:

1. **Addition** — Adds two numbers.
2. **Subtraction** — Subtracts the second number from the first.
3. **Multiplication** — Multiplies two numbers.
4. **Division** — Divides the first number by the second. Division by zero returns `Infinity` or `NaN` depending on the input.
5. **Modulus** — Calculates the remainder using both `double` and `decimal` types. Handles precision issues and exceptions.
6. **Exponentiation** — Raises a base number to a given exponent.
7. **Square Root** — Calculates the square root of a number. Negative input returns `NaN`.

## Project Structure

- `Program.cs` — Entry point that starts the calculator.
- `CalculatorApp.cs` — Handles user input and interaction.
- `Calculator.cs` — Contains all mathematical operations.

## Input Validation

- All numeric inputs are validated using `double.TryParse`.
- If the input is too large, a warning is shown (e.g., `Infinity`).
- Invalid inputs ask the user to try again.

## Notes

- Division by zero does not throw an exception when working with double values in C#.
- Division and modulus with `decimal` throw exceptions when dividing by zero.
- In the console, we get Infinity when the double value which is entered is greater than double.MaxValue or less than double.MinValue.
- Modulus with `double` may lose precision. The result is also calculated using `decimal` for better accuracy.
- Modulus operations with `double` values may produce unexpected results due to loss of precision.
	- Example: 120 % 0.05 returns 0.04999999999999334, which is approximately 0.
- Casting from `double` to `decimal` does not result in a loss of precision if the values are up to 15 digits long.
- When a `double` is explicitly cast to `decimal` for a modulus operation, an OverflowException may occur if the original value is too large or too small for the decimal type.
- Square root of a negative number returns `NaN`.

## How to Run

1. Open the solution in Visual Studio.
2. Set the project as the startup project.
3. Build the solution to ensure all files compile correctly.
4. Run the application using `Ctrl + F5` or the green "Start" button.
5. Follow the on-screen instructions in the console to perform calculations.