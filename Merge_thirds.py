import random
import time

list = []
n = 101
for i in range(n):
    list.append(i)
random.shuffle(list)
print("Unsorted list:", list)

def Split(list):
    first = len(list) // 3
    second = 2 * (len(list) // 3)

    left = []
    middle = []
    right = []

    for i in range(first):
        left.append(list[i])
    for i in range(first, second):
        middle.append(list[i])
    for i in range(second, len(list)):
        right.append(list[i])

    return left, middle, right

def Merge(left, middle, right):
    merged = []
    l = m = r = 0

    while l < len(left) and m < len(middle) and r < len(right):
        if left[l] <= middle[m] and left[l] <= right[r]:
            merged.append(left[l])
            l += 1
        elif middle[m] <= left[l] and middle[m] <= right[r]:
            merged.append(middle[m])
            m += 1
        else:
            merged.append(right[r])
            r += 1

    for i in range(l, len(left)):
        merged.append(left[i])

    for i in range(m, len(middle)):
        merged.append(middle[i])

    for i in range(r, len(right)):
        merged.append(right[i])

    return merged

def MergeSort(list):
    if len(list) <= 1:
        return list 

    if len(list) == 2:
        return sorted(list) #trochu podvádění ale jinak se mi to zacyklilo

    left, middle, right = Split(list)
    sortedLeft = MergeSort(left)
    sortedMiddle = MergeSort(middle)
    sortedRight = MergeSort(right)

    return Merge(sortedLeft, sortedMiddle, sortedRight)

start_time = time.time()
sorted_list = MergeSort(list)
end_time = time.time()
elapsed_time = end_time - start_time

print("Sorted list:", sorted_list)
print(f"Function MergeSort with {n} numbers executed in {elapsed_time:.6f} seconds.")
