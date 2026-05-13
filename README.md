# VIN Control Digit Calculator (Kalkulator VIN)

A simple desktop application built with C# and Windows Forms that calculates the check digit (control digit) of a Vehicle Identification Number (VIN).

## Features

* **Interactive Input:** 16 separate input fields for the VIN characters (excluding the check digit position).
* **Input Validation:** Automatically restricts input to valid alphanumeric characters and converts them to uppercase.
* **VIN Rules Enforced:** Automatically prevents typing illegal VIN characters (`I`, `O`, `Q`).
* **Auto-Focus:** Automatically moves the cursor to the next textbox after entering a character, streamlining the typing experience.
* **Real-time Calculation:** The check digit is calculated and displayed automatically as soon as all 16 valid characters are entered.
* **Quick Reset:** A clear button allows you to instantly empty all fields and start over.

## How it Works

The application uses the standard VIN check digit calculation algorithm:
1. Each letter is assigned a specific numeric value.
2. Each position in the input is multiplied by a specific mathematical weight (`8, 7, 6, 5, 4, 3, 2, 10, 9, 8, 7, 6, 5, 4, 3, 2`).
3. The sum of these products is calculated and divided by 11.
4. The remainder determines the check digit (if the remainder is `10`, the check digit is `X`).

## Technologies Used
* C#
* .NET Framework
* Windows Forms

## Getting Started
1. Clone the repository.
2. Open `KalkulatorVin.sln` in Visual Studio.
3. Build and run the project.
