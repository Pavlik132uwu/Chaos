import random
import time

list=[]
n=10001
for i in range(n):
    list.append(i)
random.shuffle(list)
print("Unsorted list:", list)

def Split(list):
    middle=len(list)//2
    left=[]
    right=[]
    for i in range(middle):
        left.append(list[i])
        right.append(list[middle+i])
    if len(list)%2==1:
        right.append(list[len(list)-1])
    return left, right

def Merge(left, right):
    merged = []
    l = r = 0

    while l < len(left) and r < len(right):
        if left[l] <= right[r]:
            merged.append(left[l])
            l += 1
        else:
            merged.append(right[r])
            r += 1

    for k in range(l, len(left)):  
        merged.append(left[k])

    for k in range(r, len(right)):  
        merged.append(right[k])
    return merged

def MergeSort(list):
    if len(list) <= 1:
        return list
    
    left, right = Split(list)
    sortedLeft=MergeSort(left)
    sortedRight=MergeSort(right)

    return Merge(sortedLeft, sortedRight)

start_time = time.time() # Record start t
print("Sorted list", MergeSort(list))
end_time = time.time() # Record end time
elapsed_time = end_time - start_time # Calculate elapsed time 
print(f"Function MergeSort with {n} numbers executed in {elapsed_time:.6f} seconds.")