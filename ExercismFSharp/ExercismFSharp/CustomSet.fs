module CustomSet

// Space used: O(n)
type Set<'a> = { set : 'a list }

// Initializers
// Complexity: O(1)
let empty =
    { set = [] }

// Complexity: O(1)
let singleton elem =
    { set = [elem] }

// Complexity: O(n log n)
let fromList list =
    { set = list |> List.distinct |> List.sort }

// Operations on a set
// Complexity: O(1) 
let isEmpty set =
    List.length set.set = 0

// Complexity: O(n)
let contains elem set =
    List.contains elem set.set

// Complexity: O(n log n)
let insert elem set =
    fromList (elem :: set.set)

// Comparison of sets
// Complexity: O(n^2)
let isSubsetOf first second =
    List.forall
        (fun elem -> contains elem second)
        first.set

// Complexity: O(n^2)
let isDisjointFrom first second =
    List.forall
        (fun elem -> contains elem second |> not)
        first.set

// Complexity: O(n^2)
let intersection first second =
    let result =
        List.filter
            (fun elem -> List.contains elem second.set)
            first.set
    fromList result

// Complexity: O(n^2)
let difference first second =
    fromList (List.except second.set first.set)

// Complexity: O(n log n)
let union first second =
    fromList (first.set @ second.set)