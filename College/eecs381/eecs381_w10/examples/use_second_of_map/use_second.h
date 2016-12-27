#ifndef USE_SECOND_H
#define USE_SECOND_H

#include <utility>
#include <functional>

/*  use_second  - an adapter that calls a function or function object with the second of a supplied pair.

Let map<K, V> m be a map container.

Let f be a function that takes an argument of type V when called. Then
for_each(m.begin(), m.end() use_second(f));
will call f with the second of each pair in the container.

Let fo be a function object whose class operator() takes an argument of type V when called,
and which is an adaptable function object class. Then:
for_each(m.begin(), m.end() use_second(fo));
will call fo with the second of each pair in the container.

The use_second adapter is not adaptable, and so has to be the outermost adapter in the algorithm call,
which it normally would be for algorithms whose dereferenced iterator is a pair.

This was inspired by, but significantly modified from, the with_data adapter presented by 
Jay Kint at http://www.codeproject.com/vcpp/stl/call_with.asp

Unlike Kint's version, this version allows the called function to modify the second of the pair,
and enforces the argument type of the called function directly in the operator() templated function.

- David Kieras, 10/17/2006

*/

// Function object for calling operator() of a function object using the second of a pair.
// The supplied function object must be adaptable, but this function object is not.
// Therefore it has to be the outermost adapter in the algorithm call.
template <typename F>
class use_second_FO_t {
public:
    typedef typename F::result_type Result;	// supplied function object must be adaptable
    typedef typename F::argument_type Arg;

    explicit use_second_FO_t(F f_) : f(f_) {}
    
    template <typename K>
    Result operator() (std::pair<K, Arg>& the_pair) const 
		{
			return f(the_pair.second);
		}

    F f;
};

// function template for creating the function object
template <typename F>
use_second_FO_t<F> use_second(F f)
{
    return use_second_FO_t<F>(f);
}


// Function object for calling a function directly using the second of a pair
// This is not adaptable - it has to be the outermost adapter in the algorithm call.
template <typename Result, typename Arg>
class use_second_func_t {
public:

    typedef Result (*F)(Arg);

    explicit use_second_func_t(F f_) : f(f_) {}
    
    template <typename K>
    Result operator() (std::pair<K, Arg>& the_pair) const 
		{return f(the_pair.second);}

    F f;
};

// function template for creating the function object
template <typename Result, typename Arg>
use_second_func_t<Result, Arg> use_second(Result (*f)(Arg))
{
    return use_second_func_t<Result, Arg>(f);
}


#endif


