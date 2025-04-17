using System;
using System.Collections.Generic;
using UnityEngine;

public class RPNEvaluator
{
    public static int Evaluate(string expression, Dictionary<string, int> variables = null)
    {
        if (string.IsNullOrEmpty(expression))
            return 0;

        if (expression == "base" && variables != null && variables.ContainsKey("base"))
            return variables["base"];

        Stack<int> stack = new Stack<int>();
        string[] tokens = expression.Split(' ');

        foreach (string token in tokens)
        {
            if (IsOperator(token))
            {
                if (stack.Count < 2)
                    throw new ArgumentException($"Invalid RPN expression: {expression}. Not enough operands for operator {token}.");

                int b = stack.Pop();
                int a = stack.Pop();
                stack.Push(ApplyOperator(a, b, token));
            }
            else if (int.TryParse(token, out int value))
            {
                stack.Push(value);
            }
            else if (variables != null && variables.ContainsKey(token))
            {
                stack.Push(variables[token]);
            }
            else
            {
                throw new ArgumentException($"Invalid token in RPN expression: {token}");
            }
        }

        if (stack.Count != 1)
            throw new ArgumentException($"Invalid RPN expression: {expression}. Stack contains {stack.Count} elements after evaluation.");

        return stack.Pop();
    }

    private static bool IsOperator(string token)
    {
        return token == "+" || token == "-" || token == "*" || token == "/" || token == "%";
    }

    private static int ApplyOperator(int a, int b, string op)
    {
        return op switch
        {
            "+" => a + b,
            "-" => a - b,
            "*" => a * b,
            "/" => a / b,
            "%" => a % b,
            _ => throw new ArgumentException($"Unsupported operator: {op}")
        };
    }
}