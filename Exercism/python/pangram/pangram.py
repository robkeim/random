def is_pangram(sentence):
    sentence = set(sentence.lower())
    
    for c in "abcdefghijklmnopqrstuvwxyz":
        if not c in sentence:
            return False
        
    return True
