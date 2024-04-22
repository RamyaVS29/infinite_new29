<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EvenOdd.aspx.cs" Inherits="JavaScriptAssesment1.EvenOdd" %>

<!DOCTYPE html>
<html>
<head>
    <title>Odd Even Checker</title>
</head>
<body>
    <h2 style="color:darkgoldenrod;">Odd Even Checker</h2>
    <div id="output"></div>

    <script>
        
        function checkOddEven(number) {
            return number % 2 === 0 ? "Even" : "Odd";
        }

      
        function displayResult(number, result) {
            document.getElementById('output').innerHTML += number + ' is ' + result + '<br>';
        }

      
        for (var i = 0; i <= 15; i++) {
            var result = checkOddEven(i);
            displayResult(i, result);
        }
    </script>
</body>
</html>
