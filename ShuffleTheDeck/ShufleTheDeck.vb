'Noah Holloway
'RCET 2265
'Spring 2025
'Shuffle The Deck
Option Strict On
Option Explicit On
Option Compare Text
Module ShufleTheDeck
    Sub Main()
        Dim userInput As String

        Do
            Console.Clear()
            DisplayDeck()
            Console.WriteLine()
            Console.WriteLine("Enter 'd' to deal a card, 's' to shuffle, 'q' to quit")
            userInput = Console.ReadLine()

            Select Case userInput
                Case "d"
                    If Not DealCard() Then
                        Console.WriteLine("No cards left! Enter 's' to shuffle or 'q' to quit.")
                    End If
                Case "s"
                    CardTracker(0, 0,, True)
                    ResetCardCounter()
                    Console.WriteLine("Deck shuffled!")
                Case "q"
                    Exit Do

            End Select

            Console.WriteLine("Press Enter to continue...")
            Console.ReadLine()

        Loop Until userInput = "q"

        Console.Clear()
    End Sub

    Function DealCard(Optional clearCount As Boolean = False) As Boolean
        Dim temp(,) As Boolean = CardTracker(0, 0)
        Dim suitIndex As Integer
        Dim valueIndex As Integer
        Static cardCounter As Integer

        If clearCount Then
            cardCounter = 0
            Return True
        Else
            If cardCounter >= 52 Then
                Return False
            End If

            Do
                suitIndex = RandomNumberBetween(0, 3)
                valueIndex = RandomNumberBetween(0, 12)
            Loop Until temp(suitIndex, valueIndex) = False

            CardTracker(suitIndex, valueIndex, True)
            cardCounter += 1
            Console.WriteLine($"Dealt: {FormatCard(suitIndex, valueIndex)}")
            Return True
        End If
    End Function

    Function CardTracker(suitIndex As Integer, valueIndex As Integer, Optional update As Boolean = False, Optional clear As Boolean = False) As Boolean(,)
        Static _cardTracker(3, 12) As Boolean

        If update Then
            _cardTracker(suitIndex, valueIndex) = True
        End If

        If clear Then
            ReDim _cardTracker(3, 12)
        End If

        Return _cardTracker
    End Function

    Sub ResetCardCounter()
        DealCard(True)
    End Sub

    Sub DisplayDeck()
        Dim suits() As String = {"Spades", "Clubs", "Hearts", "Diamonds"}
        Dim values() As String = {"2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"}
        Dim tracker(,) As Boolean = CardTracker(0, 0)
        Dim columnWidth As Integer = 10

        Console.WriteLine(StrDup(columnWidth * 14, "-"))

        For suitIndex As Integer = 0 To 3
            Console.Write(suits(suitIndex).PadLeft(columnWidth) & "|")
            For valueIndex As Integer = 0 To 12
                Dim displayString As String
                If tracker(suitIndex, valueIndex) Then
                    displayString = "Dealt"
                Else
                    displayString = values(valueIndex)
                End If
                Console.Write(displayString.PadLeft(columnWidth))
            Next
            Console.WriteLine()
        Next
        Console.WriteLine(vbNewLine & StrDup(columnWidth * 14, "-"))
    End Sub

    Function FormatCard(suitIndex As Integer, valueIndex As Integer) As String
        Dim suits() As String = {"Spades", "Clubs", "Hearts", "Diamonds"}
        Dim values() As String = {"2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"}
        Return values(valueIndex) & " of " & suits(suitIndex)
    End Function

    Function RandomNumberBetween(min As Integer, max As Integer) As Integer
        Static rnd As New Random()
        Return rnd.Next(min, max + 1)
    End Function
End Module

