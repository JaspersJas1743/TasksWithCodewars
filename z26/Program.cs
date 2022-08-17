using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#region Exercise
/*
In this Kata, you're going to transpile an expression from one langauge into another language.
The source language looks like Kotlin and the target language looks like Dart. And you don't need to know neither of them to complete this Kata.
We're going to transpile a function call expression.
If you successfully parsed the input, return Right output, otherwise give me Left "Hugh?".
For C#, return the empty string upon failure, else the transpiled string.
We have three kinds of basic expressions:
1. names, like abc, ABC, run, a1, beginning with _/letters and followed by _/letters/numbers
2. numbers, like 123, 2333, 66666
3. lambda expressions, like { a -> a }, { a, b -> a b }(source), (a){a;}, (a,b){a;b;}(target)
We have empty characters blank space and \n.
The definition of names is quite similiar to C/Java. Names like this are invalid: 1a
You don't have to worry about reserved words here.
Lambda expressions consist of two parts:
1. parameters, they're just names/numbers
2. statements, a list of names/numbers, seperated by whitespaces in source language, by ; in target language.
Invoking a function is to pass some arguments to something callable(names and lambdas), like plus(1, 2), or repeat(10, { xxx }).
There's a syntax sugar in Kotlin: if the last argument is a lambda, it can be out of the brackets. Like, repeat(10, { xxx }) 
can be written in repeat(10) { xxx }. And if that lambda is the only argument, you can even ignore the brackets. Like: run({ xxx }) is equaled to run { xxx }.
You can refer to the examples at the bottom.

The source language looks like:
function ::= expression "(" [parameters] ")" [lambda]
           | expression lambda
expression ::= nameOrNumber
             | lambda
parameters ::= expression ["," parameters]
lambdaparam ::= nameOrNumber ["," lambdaparam]
lambdastmt  ::= nameOrNumber [lambdastmt]
lambda ::= "{" [lambdaparam "->"] [lambdastmt] "}"
Notice: there can be whitespaces among everywhere, it's not a part of the language grammar.

The target language looks like:
function ::= expression "(" [parameters] ")"
expression ::= nameOrNumber
             | lambda
parameters ::= expression ["," parameters]
lambdaparam ::= nameOrNumber ["," lambdaparam]
lambdastmt  ::= nameOrNumber ";" [lambdastmt]
lambda ::= "(" [lambdaparam] "){" [lambdastmt] "}"

You shouldn't produce any whitespaces in the target language.
Those examples covered all the language features shown above. Hope you enjoy it :D
fun() => fun()
fun(a) => fun(a)
fun(a, b) => fun(a,b)
{}() => (){}()
fun {} => fun((){})
fun(a, {}) => fun(a,(){})
fun(a) {} => fun(a,(){})
fun {a -> a} => fun((a){a;})
{a -> a}(1) => (a){a;}(1)
fun { a, b -> a b } => fun((a,b){a;b;})
{a, b -> a b} (1, 2) => (a,b){a;b;}(1,2)
f { a } => f((){a;})
f { a -> } => f((a){})
{}{} => (){}((){})
You have to write your own tokenizer (hint: whitespace is significant to separate some tokens, but can be ignored otherwise).
*/
#endregion 

namespace z26
{
    public class Transpiler
    {
        private static string TranspileCurlyBrackets(string expression)
        {
            if (string.IsNullOrEmpty(expression)) return "(){}";
            
            string[] attribute = expression.Split("->", StringSplitOptions.TrimEntries);
            string? attributeInRoundBrackets = attribute.FirstOrDefault()?.Replace(" ", ""),
                    attributeInCurlyBrackets = string.Join("", (attribute.LastOrDefault() ?? "").Split(
                        " ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
                    ).Select(x => $"{x};"));

            if (!expression.Contains("->")) attributeInRoundBrackets = string.Empty;
            
            return $"({attributeInRoundBrackets}){{{attributeInCurlyBrackets}}}";
        }

        private static string TranspileLambdaExpression(string expression)
        {
            string[] func = expression.Split(new char[]{'(', ')'}, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);                
            var allCurlyBrackets = func.Where(x => (x?.Contains('{') ?? false))
                    .Select(x => x?.Split('{', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                        .Where(x => x.Contains('}') || x.Contains('}'))
                        .Select(x => string.Join("", x.SkipLast(1)).Replace("}", "").Replace("{", "")
                    )
                );
            var brackets = allCurlyBrackets.Aggregate((x, y) => x?.Concat(y ?? Enumerable.Empty<string>()).ToArray())?.Select(x => TranspileCurlyBrackets(x));
            string? funcAttribute = func.Last().Split(new char[]{'{', '}'}, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).FirstOrDefault(),
                    simpleAttribute = string.Empty;

            if ((funcAttribute?.Contains(",") ?? false) && funcAttribute != "," && (!funcAttribute?.Contains("->") ?? false)) simpleAttribute = funcAttribute;

            return $"{func.First()}({simpleAttribute}{string.Join(",", (brackets ?? Enumerable.Empty<string>()))})";
        }

        private static string TranspileLambdaExpressionAreNotInsideBrackets(string expression)
        {
            string[] func = expression.Split('{', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            string brackets = TranspileCurlyBrackets(func.Last().Replace("}", "")),
                    funcName = func.First().Replace(" ", ""), start = $"{funcName}(";            
            
            if (expression.Contains('('))
            {
                start = $"{funcName[0..^1]}";
                if (expression[expression.IndexOf('(') + 1] != ')') start += ',';
            }

            return $"{start}{brackets})";
        }

        private static string TranspileInvokingLambdaDirectly(string expression)
        {
            string[] func = expression.Split("}", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            string  brackets = TranspileCurlyBrackets(func.First().Replace("{", "")),
                    roundBrackets = func.Last().Replace(" ", "");
            
            if (roundBrackets.Contains('(') && roundBrackets.Contains('{')) roundBrackets = TranspileLambdaExpressionAreNotInsideBrackets(roundBrackets + "}");
            
            return roundBrackets.Contains('{') && !roundBrackets.Contains('(') ? string.Empty : (brackets + roundBrackets);
        }

        public static string transpile(string expression)
        {
            if (new string[]{"()", "{}"}.Any(x => x == expression)) return string.Empty;
            Console.WriteLine($"expression is {expression}");
            expression = Regex.Replace(expression.Replace("\n", ""), "\\s+", " ");
            string  oneOrMoreLetter = @"\s*([_a-zA-Z]+\d*|[_a-zA-Z]*\d+)\s*", nullOrMoreLetter = @"\s*[_a-zA-Z]*\d*\s*",
                    roundBrackets = @$"\s*(\({oneOrMoreLetter}(,{oneOrMoreLetter})*\)|\(\s*\))\s*",
                    curlyBrackets = @$"\s*(\{{({oneOrMoreLetter})(?=((->)?|(,{oneOrMoreLetter})*))((,{oneOrMoreLetter})*)(?=(->))(->)?({nullOrMoreLetter})*\}}|\{{({nullOrMoreLetter})*\}})\s*",
                    simpleExpression = @$"^{oneOrMoreLetter}{roundBrackets}$",
                    lambdaExpression = @$"^{nullOrMoreLetter}\(({oneOrMoreLetter},)*({curlyBrackets}(,{curlyBrackets})*)+\)$",
                    lambdaExpressionAreNotInsideBrackets = @$"^({oneOrMoreLetter}({roundBrackets})*|{nullOrMoreLetter}{roundBrackets})({curlyBrackets}(,{curlyBrackets})*)+$",
                    invokingLambdaDirectly = @$"^({curlyBrackets})+({roundBrackets})?({curlyBrackets})?$";
            int countArguments = string.Join("", expression.Split(new char[]{'(', ')'}, StringSplitOptions.RemoveEmptyEntries).Skip(1))
                    .Split(new string[]{" ", ","}, StringSplitOptions.RemoveEmptyEntries).Count(x => new string[]{",", " "}.All(y => y != x)),
                countCommas = expression.Count(x => x == ',');

            if (new string[]{simpleExpression, lambdaExpression, lambdaExpressionAreNotInsideBrackets, invokingLambdaDirectly}
                .All(x => !Regex.IsMatch(expression, x))) return string.Empty;
            if (Regex.IsMatch(expression, lambdaExpression)) return TranspileLambdaExpression(expression);
            if (Regex.IsMatch(expression, lambdaExpressionAreNotInsideBrackets)) return TranspileLambdaExpressionAreNotInsideBrackets(expression);
            if (Regex.IsMatch(expression, invokingLambdaDirectly)) return TranspileInvokingLambdaDirectly(expression);
            if (countCommas != countArguments - 1 && countArguments != 0) return string.Empty;
            
            return expression.Replace(" ", "");
        }
    }
    
    internal class Program
    {
        public static bool fromTo(string input, string expected) => Transpiler.transpile(input) == expected;
        public static void Main(string[] args)
        {
            Console.WriteLine($"1. {fromTo ("run(a){as we can}", "run(a,(){as;we;can;})")}");
            Console.WriteLine($"2. {fromTo ("call(ab, cd)", "call(ab,cd)")}");
            Console.WriteLine($"3. {fromTo ("f{a, b -> a}", "f((a,b){a;})")}");
            Console.WriteLine($"4. {fromTo ("invoke  (       a    ,   b   ) { } ", "invoke(a,b,(){})")}");
            Console.WriteLine($"5. {fromTo("fun()", "fun()")}");
            Console.WriteLine($"6. {fromTo("fun(as)", "fun(as)")}");
            Console.WriteLine($"7. {fromTo("invoke({as,bs -> as bs})", "invoke((as,bs){as;bs;})")}");
            Console.WriteLine($"8. {fromTo("invoke({},{})", "invoke((){},(){})")}");
            Console.WriteLine($"9. {fromTo("fun(as, bs)", "fun(as,bs)")}");
            Console.WriteLine($"10. {fromTo("{}()", "(){}()")}");
            Console.WriteLine($"11. {fromTo("fun(as, {})", "fun(as,(){})")}");
            Console.WriteLine($"12. {fromTo("fun(as) {}", "fun(as,(){})")}");
            Console.WriteLine($"13. {fromTo("fun{}", "fun((){})")}");
            Console.WriteLine($"14. {fromTo("fun {as -> as}", "fun((as){as;})")}");
            Console.WriteLine($"15. {fromTo("{as -> as}(1)", "(as){as;}(1)")}");
            Console.WriteLine($"16. {fromTo("fun { as, bs -> as bs }", "fun((as,bs){as;bs;})")}");
            Console.WriteLine($"17. {fromTo("{as, bs -> as bs} (1, 2)", "(as,bs){as;bs;}(1,2)")}");
            Console.WriteLine($"18. {fromTo("f { as }", "f((){as;})")}");
            Console.WriteLine($"19. {fromTo("f({ as -> })", "f((as){})")}");
            Console.WriteLine($"20. {fromTo("{}{}", string.Empty)}");
            Console.WriteLine($"21. {fromTo("fun {}", "fun((){})")}");
            Console.WriteLine($"22: {fromTo ("call({})", "call((){})")}");
            Console.WriteLine($"23: {fromTo ("_()", "_()")}");
            Console.WriteLine($"24: {fromTo ("f({_->})", "f((_){})")}");
            Console.WriteLine($"25: {fromTo ("f({1a->a})", string.Empty)}");            
            Console.WriteLine($"26. {fromTo ("{a->a}(cde,y,z){x,y,d -> stuff}", "(a){a;}(cde,y,z,(x,y,d){stuff;})")}");
            Console.WriteLine($"27. {fromTo ("{a->a}(cde,y,z){x y d -> stuff}", string.Empty)}");
            Console.WriteLine($"28. {fromTo ("{x y d -> stuff}()", string.Empty)}");
            Console.WriteLine($"29. {fromTo ("{a}(cde,y,z){x,y,d jj}", string.Empty)}");
        }
    }
}