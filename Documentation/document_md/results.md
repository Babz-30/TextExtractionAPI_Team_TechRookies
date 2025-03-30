# Results

## Navigation
- [Image1](#image1)
- [Image2](#image2)
- [Image3](#image3)
- [Image4](#image4)
- [Image5](#image5)
- [Image6](#image6)
- [Image7](#image7)
- [Image8](#image8)
- [Bill misaligned](#bill-misaligned)
- [Bill](#bill)
- [Passport](#passport)
- [License Plate](#license-plate)

---

## Image1

### Original Image
![Image1](../document_images/Results/image1.jpg)

### Configuration (appsettings.json)
```json
{
  "InputImage": "image1.jpg",
  "PreprocessingSettings": {
    "RotateAngles": [ -90.0, -60.0, -45.0, -30.0, -20.0, -10.0, 10.0, 20.0, 30.0, 45.0, 60.0, 90.0, 180.0 ],
    "Thresholds": [ 50, 150, 200, 250 ],
    "TargetDPIs": [ 50, 100, 200, 300 ],
    "SatFactors": [ 20.0, 10, 5.0, 0.5, 0.2 ],
    "IntensityFactors": [ 20.0, 5.0, 0.5, 0.2 ]
  },
  "Max": 10,
  "API_URL": "https://api.openai.com/v1/embeddings",
  "EMBEDDING_MODEL": "text-embedding-ada-002",
  "MEDIA_TYPE": "application/json"
}
```

### Console Output
![Console Output](../document_images/Results/image1_console.png)

### Metrics
![Metrics](../document_images/Results/image1_metrics.png)

### Graph Representation
![Graph](../document_images/Results/image1_graph.png)

---

## Image2

### Original Image
![Image2](../document_images/Results/image2.jpg)

### Configuration (appsettings.json)
```json
{
  "InputImage": "image2.jpg",
  "PreprocessingSettings": {
    "RotateAngles": [ -90.0, -60.0, -45.0, -30.0, -20.0, -10.0, 10.0, 20.0, 30.0, 45.0, 60.0, 90.0, 180.0 ],
    "Thresholds": [ 50, 150, 200, 250 ],
    "TargetDPIs": [ 50, 100, 200, 300 ],
    "SatFactors": [ 20.0, 10.0, 5.0, 2 ],
    "IntensityFactors": [ 20.0, 10.0, 5.0, 2 ]
  },
  "Max": 30,
  "API_URL": "https://api.openai.com/v1/embeddings",
  "EMBEDDING_MODEL": "text-embedding-ada-002",
  "MEDIA_TYPE": "application/json"
}
```

### Processed Image
![Image2](../document_images/Results/processed_image2.jpg)

### Console Output
![Console Output](../document_images/Results/image2_console.png)

### Metrics
![Metrics](../document_images/Results/image2_metrics.png)

### Graph Representation
![Graph](../document_images/Results/image2_graph.png)

---

## Image3

### Original Image
![Image3](../document_images/Results/image3.jpg)

### Configuration (appsettings.json)
```json
{
  "InputImage": "image3.jpg",
  "PreprocessingSettings": {
    "RotateAngles": [ -90.0, -60.0, -45.0, -30.0, -20.0, -10.0, 10.0, 20.0, 30.0, 45.0, 60.0, 180.0 ],
    "Thresholds": [ 50, 150, 200, 250 ],
    "TargetDPIs": [ 50, 100, 200, 300 ],
    "SatFactors": [ 20.0, 10, 5.0, 0.5, 0.2 ],
    "IntensityFactors": [ 20.0, 5.0, 0.5, 0.2 ]
  },
  "Max": 30,
  "API_URL": "https://api.openai.com/v1/embeddings",
  "EMBEDDING_MODEL": "text-embedding-ada-002",
  "MEDIA_TYPE": "application/json"
}
```

### Console Output
![Console Output](../document_images/Results/image3_console.png)

### Metrics
![Metrics](../document_images/Results/image3_metrics.png)

### Graph Representation
![Graph](../document_images/Results/image3_graph.png)

---

## Image4

### Original Image
![Image4](../document_images/Results/image4.jpg)

### Configuration (appsettings.json)
```json
{
  "InputImage": "image4.jpg",
  "PreprocessingSettings": {
    "RotateAngles": [ -90.0, -60.0, -45.0, -30.0, -20.0, -10.0, 10.0, 20.0, 30.0, 45.0, 60.0, 180.0 ],
    "Thresholds": [ 50, 150, 200, 250 ],
    "TargetDPIs": [ 50, 100, 200, 300 ],
    "SatFactors": [ 20.0, 10, 5.0, 0.5, 0.2 ],
    "IntensityFactors": [ 20.0, 5.0, 0.5, 0.2 ]
  },
  "Max": 30,
  "API_URL": "https://api.openai.com/v1/embeddings",
  "EMBEDDING_MODEL": "text-embedding-ada-002",
  "MEDIA_TYPE": "application/json"
}
```

### Console Output
![Console Output](../document_images/Results/image4_console.png)

### Metrics
![Metrics](../document_images/Results/image4_metrics.png)

### Graph Representation
![Graph](../document_images/Results/image4_graph.png)

---

## Image5

### Original Image
![Image5](../document_images/Results/image5.jpg)

### Configuration (appsettings.json)
```json
{
  "InputImage": "image5.jpg",
  "PreprocessingSettings": {
   "RotateAngles": [ -90.0, -60.0, -45.0, -30.0, -20.0, -10.0, 10.0, 20.0, 30.0, 45.0, 60.0, 180.0 ],
   "Thresholds": [ 50, 150, 200, 250 ],
   "TargetDPIs": [ 50, 100, 200, 300 ],
   "SatFactors": [ 20.0, 10, 5.0, 0.5, 0.2 ],
   "IntensityFactors": [ 20.0, 5.0, 0.5, 0.2 ]
 },
  "Max": 30,
  "API_URL": "https://api.openai.com/v1/embeddings",
  "EMBEDDING_MODEL": "text-embedding-ada-002",
  "MEDIA_TYPE": "application/json"
}
```

### Console Output
![Console Output](../document_images/Results/image5_console.png)

### Metrics
![Metrics](../document_images/Results/image5_metrics.png)

### Graph Representation
![Graph](../document_images/Results/image5_graph.png)

---

## Image6

### Original Image
![Image6](../document_images/Results/image6.png)

### Configuration (appsettings.json)
```json
{
  "InputImage": "image6.png",
  "PreprocessingSettings": {
   "RotateAngles": [ -90.0, -60.0, -45.0, -30.0, -20.0, -10.0, 10.0, 20.0, 30.0, 45.0, 60.0, 180.0 ],
   "Thresholds": [ 50, 150, 200, 250 ],
   "TargetDPIs": [ 50, 100, 200, 300 ],
   "SatFactors": [ 20.0, 10, 5.0, 0.5, 0.2 ],
   "IntensityFactors": [ 20.0, 5.0, 0.5, 0.2 ]
  },
  "Max": 30,
  "API_URL": "https://api.openai.com/v1/embeddings",
  "EMBEDDING_MODEL": "text-embedding-ada-002",
  "MEDIA_TYPE": "application/json"
}
```

### Processed Image
![Image6](../document_images/Results/processed_image6.jpg)

### Console Output
![Console Output](../document_images/Results/image6_console.png)

### Metrics
![Metrics](../document_images/Results/image6_metrics.png)

### Graph Representation
![Graph](../document_images/Results/image6_graph.png)

---

## Image7

### Original Image
![Image7](../document_images/Results/image7.png)

### Configuration (appsettings.json)
```json
{
  "InputImage": "image7.png",
  "PreprocessingSettings": {
    "RotateAngles": [ -90.0, -60.0, -45.0, -30.0, -20.0, -10.0, 10.0, 20.0, 30.0, 45.0, 60.0, 180.0 ],
    "Thresholds": [ 50, 150, 200, 250 ],
    "TargetDPIs": [ 50, 100, 200, 300 ],
    "SatFactors": [ 20.0, 10, 5.0, 0.5, 0.2 ],
    "IntensityFactors": [ 20.0, 5.0, 0.5, 0.2 ]
  },
  "Max": 30,
  "API_URL": "https://api.openai.com/v1/embeddings",
  "EMBEDDING_MODEL": "text-embedding-ada-002",
  "MEDIA_TYPE": "application/json"
}
```

### Console Output
![Console Output](../document_images/Results/image7_console.png)

### Metrics
![Metrics](../document_images/Results/image7_metrics.png)

### Graph Representation
![Graph](../document_images/Results/image7_graph.png)

---

## Image8

### Original Image
![Image8](../document_images/Results/image8.jpg)

### Configuration (appsettings.json)
```json
{
  "InputImage": "image8.jpg",
  "PreprocessingSettings": {
    "RotateAngles": [ -90.0, -60.0, -45.0, -30.0, -20.0, -10.0, 10.0, 20.0, 30.0, 45.0, 60.0, 90.0, 180.0 ],
    "Thresholds": [ 50, 150, 200, 250 ],
    "TargetDPIs": [ 50, 100, 200, 300 ],
    "SatFactors": [ 20.0, 10, 5.0, 0.5, 0.2 ],
    "IntensityFactors": [ 20.0, 5.0, 0.5, 0.2 ]
  },
  "Max": 30,
  "API_URL": "https://api.openai.com/v1/embeddings",
  "EMBEDDING_MODEL": "text-embedding-ada-002",
  "MEDIA_TYPE": "application/json"
}
```

### Console Output
![Console Output](../document_images/Results/image8_console.png)

### Metrics
![Metrics](../document_images/Results/image8_metrics.png)

### Graph Representation
![Graph](../document_images/Results/image8_graph.png)

---

## Bill misaligned

### Original Image
![Image9](../document_images/Results/image9.png)

### Configuration (appsettings.json)
```json
{
  "InputImage": "image9.jpg",
  "PreprocessingSettings": {
    "RotateAngles": [ -45.0, -30.0, -20.0, -10.0, 0, 10, 20, 30, 45 ],
    "Thresholds": [ 50, 100, 200 ],
    "TargetDPIs": [ 50, 100, 200,300 ],
    "SatFactors": [ 2.0, 1.5, 0.5 ],
    "IntensityFactors": [ 5.0, 2.0, 1.5, 0.5 ]
  },
  "Max": 10, 
  "API_URL": "https://api.openai.com/v1/embeddings",
  "EMBEDDING_MODEL": "text-embedding-ada-002",
  "MEDIA_TYPE": "application/json"
}
```

### Processed Image
![Image9](../document_images/Results/processed_image9.jpg)

### Console Output
![Console Output](../document_images/Results/image9_console.png)

### Metrics
![Metrics](../document_images/Results/image9_metrics.png)

### Graph Representation
![Graph](../document_images/Results/image9_graph.png)

---

## Bill

### Original Image
![Image10](../document_images/Results/image10.png)

### Configuration (appsettings.json)
```json
{
  "InputImage": "image10.png",
  "PreprocessingSettings": {
    "RotateAngles": [ -30.0, -15.0, 15.0, 30.0 ],
    "Thresholds": [ 50, 100, 150, 200, 250 ],
    "TargetDPIs": [50, 100, 200, 300 ],
    "SatFactors": [ 20, 10, 5, 2, 1.5 ],
    "IntensityFactors": [ 2, 1.5, 0.5, 0.2]
  },
  "Max": 15,
  "API_URL": "https://api.openai.com/v1/embeddings",
  "EMBEDDING_MODEL": "text-embedding-ada-002",
  "MEDIA_TYPE": "application/json"
}
```

### Processed Image
![Image10](../document_images/Results/processed_image10.jpg)

### Console Output
![Console Output](../document_images/Results/image10_console.png)

### Metrics
![Metrics](../document_images/Results/image10_metrics.png)

### Graph Representation
![Graph](../document_images/Results/image10_graph.png)

---

## Passport

### Original Image
![Image11](../document_images/Results/image11.jpg)

### Configuration (appsettings.json)
```json
{
  "InputImage": "image11.jpg",
  "PreprocessingSettings": {
    "RotateAngles": [ -30.0, -15.0, 15.0, 30.0 ],
    "Thresholds": [ 50, 100, 150, 200, 250 ],
    "TargetDPIs": [50, 70, 100, 200, 300 ],
    "SatFactors": [ 20, 10, 5, 2, 1.5 ],
    "IntensityFactors": [ 2, 1.5, 0.5, 0.2]
  },
  "Max": 10,
  "API_URL": "https://api.openai.com/v1/embeddings",
  "EMBEDDING_MODEL": "text-embedding-ada-002",
  "MEDIA_TYPE": "application/json"
}
```

### Processed Image
![Image11](../document_images/Results/processed_image11.jpg)

### Console Output
![Console Output](../document_images/Results/image11_console.png)

### Metrics
![Metrics](../document_images/Results/image11_metrics.png)

### Graph Representation
![Graph](../document_images/Results/image11_graph.png)

---

## License Plate

### Original Image
![Image11](../document_images/Results/image12.jpg)

### Configuration (appsettings.json)
```json
{
  "InputImage": "image12.jpg",
  "PreprocessingSettings": {
    "RotateAngles": [ -45.0, -30.0, -20.0, -10.0, 0, 10, 20, 30, 45 ],
    "Thresholds": [ 50, 100, 200 ],
    "TargetDPIs": [ 50, 100, 200,300 ],
    "SatFactors": [ 2.0, 1.5, 0.5 ],
    "IntensityFactors": [ 5.0, 2.0, 1.5, 0.5 ]
  },
  "Max": 10,
  "API_URL": "https://api.openai.com/v1/embeddings",
  "EMBEDDING_MODEL": "text-embedding-ada-002",
  "MEDIA_TYPE": "application/json"
}
```

### Processed Image
![Image11](../document_images/Results/processed_image12.jpg)

### Console Output
![Console Output](../document_images/Results/image12_console.png)

### Metrics
![Metrics](../document_images/Results/image12_metrics.png)

### Graph Representation
![Graph](../document_images/Results/image12_graph.png)

---
