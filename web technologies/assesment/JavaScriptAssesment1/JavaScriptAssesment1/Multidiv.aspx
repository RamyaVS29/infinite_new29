<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Multidiv.aspx.cs" Inherits="JavaScriptAssesment1.Multiplication" %>

<!DOCTYPE html>
<html>
<head>
    <title>My Calculator</title>
</head>
<body>
    <h2 style="color:darkmagenta ;">My Calculator</h2>

    <form id="MycalculatorForm">
        <label for="num1">Enter the first Number:</label>
        <input type="Number" id="num1" name="num1" required><br><br>
        
        <label for="num2">Enter the second number:</label>
        <input type="Number" id="num2" name="num2" required><br><br>
        
        <button type="button" onclick="calculate()">Calculate</button>
    </form>

    <div id="result"></div>

    <script>
        function calculate() {
          
            var num1 = parseFloat(document.getElementById('num1').value);
            var num2 = parseFloat(document.getElementById('num2').value);

           
            if (isNaN(num1) || isNaN(num2)) {
                document.getElementById('result').innerHTML = 'Please enter valid numbers.';
                return;
            }

          
            var multiplication = num1 * num2;
            var division = num1 / num2;

         
            document.getElementById('result').innerHTML = 'Multiplication: ' + multiplication + '<br>Division: ' + division;
        }
    </script>
</body>
</html>
