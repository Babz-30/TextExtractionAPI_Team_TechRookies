import matplotlib.pyplot as plt
import numpy as np
import pandas as pd

# Data provided
data = {
    'Technique':   ["rotated_30_resized_200", "rotated_30_resized_300", "rotated_30_resized_100", "rotated_30_resized_50", "rotated_-60_resized_300", "rotated_-60_resized_100", "rotated_-60_resized_50", "rotated_-60_resized_200"],
    'DictionaryAccuracy':   [0.8571, 0.8421, 0.8393, 0.8214, 0.2000, 0.1556, 0.1176, 0.1064],
    'MeanConfidence':   [0.8628, 0.8782, 0.8785, 0.7546, 0.4791, 0.4720, 0.3911, 0.5038],
    'MeanCosineSimilarity':    [0.8470, 0.8450, 0.8371, 0.8396, 0.8288, 0.8192, 0.8162, 0.8343]
}

# Convert the data into a pandas DataFrame
df = pd.DataFrame(data)

# Sort the DataFrame by the columns in descending order to find the best technique based on all metrics
df_sorted = df.sort_values(by=['DictionaryAccuracy', 'MeanConfidence'], ascending=[False, False])

# The best preprocessing technique is now the first row of the sorted DataFrame
best_technique = df_sorted.iloc[0]

# Plot the bar graph
fig, ax = plt.subplots(figsize=(10, 6))

# Define positions for the bars
bar_width = 0.25
index = np.arange(len(df))

# Create bars for MeanCosineSimilarity, DictionaryAccuracy, and MeanConfidence
bar1 = ax.bar(index - bar_width, df['MeanCosineSimilarity'], bar_width, label='MeanCosineSimilarity', color='lightblue')
bar2 = ax.bar(index, df['DictionaryAccuracy'], bar_width, label='DictionaryAccuracy', color='lightgreen')
bar3 = ax.bar(index + bar_width, df['MeanConfidence'], bar_width, label='MeanConfidence', color='lightcoral')

# Highlight the best technique
best_technique_idx = df[df['Technique'] == best_technique['Technique']].index[0]
bar1[best_technique_idx].set_color('blue')
bar2[best_technique_idx].set_color('green')
bar3[best_technique_idx].set_color('red')

# Annotate the best technique
best_value = best_technique['DictionaryAccuracy']
plt.text(best_technique_idx, best_value + 0.01, f'Best: {best_technique["Technique"]}',
         ha='center', va='bottom', color='black', fontweight='bold')

# Labeling the plot
ax.set_xlabel('Technique')
ax.set_ylabel('Values')
ax.set_title('Comparison of Top 10 Techniques Based on MeanCosineSimilarity, DictionaryAccuracy, and MeanConfidence')
ax.set_xticks(index)
ax.set_xticklabels(df['Technique'], rotation=45, ha='right')
ax.legend()

# Display the plot
plt.tight_layout()
plt.show()

# Print the best preprocessing technique
print(f"The best preprocessing technique is: {best_technique['Technique']}")
