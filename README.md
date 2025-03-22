# Project Title: ML 24/25-10 Creating Text from Images with OCR API

## Problem Statement
The goal of this project is to implement an OCR solution that leverages the Tesseract SDK. The C# application should load images from an input folder. The task is to develop an application that preprocesses images by shifting, rotating, or applying other suitable transformations. After preprocessing, the application should extract text using the Tesseract API. The quality of the extracted text depends on factors such as image quality, lighting conditions, the angle of the image, and potentially other variables, which should be assessed during the project. The final solution must function as a console application that accepts various parameters. The output should be clear and include the extracted text from different pre-processed images. Additionally, the final result must provide a comparison of the extraction quality between various preprocessing approaches.

## Introduction
We have developed an advanced text extraction application that leverages the Tesseract OCR SDK for extracting text from images. The process begins with a series of preprocessing techniques designed to enhance the quality of the image and improve OCR accuracy. These preprocessing steps include combination of:

- **Image rotation**: Adjusting image orientation to standardize text positioning.
- **HSI (Hue, Saturation, Intensity) color space transformations**: Adjusting the image's color balance.
- **Inversion**: Swapping the text and background colors.
- **Resizing**: Adjusting the image based on DPI (dots per inch) resolution to ensure optimal scaling for text extraction.
- **Grayscale conversion**: Reducing image complexity.
- **Binarization**: Converting the image into black and white.

Once the image is processed, text is extracted using the Tesseract OCR engine, which is a robust open-source Optical Character Recognition (OCR) tool. After text extraction, we compute metrics such as:

- **Text embedding**: Converting the text into numerical representations for analysis.
- **Cosine similarity**: Comparing the extracted text between different preprocessing technique.
- **Dictionary accuracy**: Measuring the proportion of recognized words that exist in a predefined dictionary.
- **Mean word confidence**: The average confidence score of the words detected by the OCR system.

The calculated metrics are then saved in a [`ExtractedTextMeanConfidence.csv`](Source/OCRApplication/OCRApplication/Output/Tesseract_Output/ExtractedTextMeanConfidence.csv) and [`CosineSimilarityMatrix.csv`](Source/OCRApplication/OCRApplication/Output/Cosine_Similarity_Output/CosineSimilarityMatrix.csv) file for further analysis and reporting. Through data mining techniques, we handle and filter out false similarity results to improve accuracy. The best preprocessing technique is identified based on the above metrics, and the extracted text is displayed on the console for the user to review. This application provides an efficient and reliable solution for text extraction from images, offering a detailed analysis of OCR accuracy through computational metrics.

## Flowchart
<p align="center">
  <img src="/Documentation/document_images/flowchart.png" alt="Flowchart">
</p>
<p align="center"><i>Figure 1: Project Overview</i></p>

## Code Overview
Refer [Documentation](/Documentation/document_md/code_overview.md) for methodology and code overview.

## Getting Started
Refer to the [Getting Started Guide](/Documentation/document_md/getting_started.md) for instructions on how to set up and run the application.

## Results
The extracted text, preprocessed images, and computational metrics are stored in the `Output` folder.

### **📌 Access the Results**
- 📷 **Preprocessed Images:** [`Output/Preprocessed_Image_Output`](Source/OCRApplication/OCRApplication/Output/Preprocessed_Image_Output)
- 📝 **Extracted Text & Metrics:** [`Output/Tesseract_Output`](Source/OCRApplication/OCRApplication/Output/Tesseract_Output)
- 📊 **Cosine Similarity Matrix:** [`Output/Cosine_Similarity_Output/CosineSimilarityMatrix.csv`](Source/OCRApplication/OCRApplication/Output/Cosine_Similarity_Output/)

### 📂 Test Results Documented
Refer [Results](/Documentation/document_md/results.md) for sample image input and output. 
