# Development Specs for Multivector Interface

## General

A multivector object contains an internal storage to hold its scalar coefficients and basis blade IDs.

Internal storage can be:
- A full list of coefficients suitable for small multivectors
- A sparse list of coefficients (a dictionary) for multivectors with no specific sparsity pattern
- A graded list of coeficients for a multivector with graded sparsity
- A binary tree
- A single term
- A single grade multivector

Internal storage sparsity structure is immutable but coefficients values are mutable.
In-place operations on scalar cpefficients are possible.
To create a new multivector with different or unknown sparsity structure we need multivector factories.
A multivector factory can accumulate terms into the desired sparse storage internally.

Operations on multivectors:
- In-place operations change scalar values without changing sparsity structure. This includes negation, grade involutions, etc.
- Sprasity preserving operations create new multivector storage with same sparsity structure as source and modify scalar values.
- Sparsity altering operations create a factory to accumulate the resulting terms.
- 