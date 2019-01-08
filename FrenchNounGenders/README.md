# Guessing French noun genders

This project was an attempt to find the best algorithm to "guess" the gender for a French noun. I've found that one of the trickiest things in learning French has been to remember the gender for all of the nouns. The only correct way to do it is to memorize them, but what happens if you want to use a word while you're speaking and can't remember its gender? Is there a way to guess more accurately than a coin toss?

I pulled over 41k nouns from this [site](http://www.dicollecte.org/download.php?prj=fr) and then retrieved their genders from [le-dictionnaire](http://www.le-dictionnaire.com). I then guessed at classifying the nouns based on different pre-defined sets of rules and calculated how accurate my results were. Here are the results of the computation:

### Always masculine

This algorithm is simply to always guess that the word was masculine which wound up with the following results:

Category | Percent correct | Raw
-- | -- | --
Overall |  55.47% | 22919 / 41321
Masculine |100.00% |22919 / 22919
Feminine | 0.00% |0 / 18402

### Always feminine

This algorithm is the opposite of the previous one to always guess that the word was feminine.

Category | Percent correct | Raw
-- | -- | --
Overall | 44.53% | 18402 / 41321
Masculine | 0.00% | 0 / 22919
Feminine | 100.00% | 18402 / 18402

### Vowels and consonants

This algorithm consisted of assuming any word ending in a consonant is masculine while any word ending in a vowel is feminine.

Category | Percent correct | Raw
-- | -- | --
Overall | 64.23% | 26540 / 41321
Masculine | 52.81% | 12104 / 22919
Feminine | 78.45% | 14436 / 18402

### Per letter

For this algorithm I assigned each ending letter to be either masculine or feminine maximizing the number of correct responses. This resulted in words ending in e, n, and w being guessed as feminine while the rest as masculine.

Category | Percent correct | Raw
-- | -- | --
Overall | 71.45% | 29524 / 41321
Masculine | 53.61% | 12287 / 22919
Feminine | 93.67% | 17237 / 18402

### French together

I pulled a list of rules for only feminine words from "A simplified list of endings" on [French together](https://frenchtogether.com/french-nouns-gender):

Feminine noun endings:
- The majority of words that end in -e or -ion.
- Except words ending in -age, -ege, -é, or -isme (these endings often indicate masculine words).

Masculine noun endings:
- Most words with other endings are masculine.

Category | Percent correct | Raw
-- | -- | --
Overall | 80.43% | 33233 / 41321
Masculine | 77.03% | 17654 / 22919
Feminine | 84.66% | 15579 / 18402

### FluentU

This algorithm consisted of matching a list of twenty-five suffixes from [FluentU](http://www.fluentu.com/french/blog/french-gender-rules).

Category | Percent correct | Raw
-- | -- | --
Overall | 77.63% | 32079 / 41321
Masculine | 70.06% | 16058 / 22919
Feminine | 87.06% | 16021 / 18402
