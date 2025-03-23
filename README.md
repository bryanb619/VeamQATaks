# VEEAM QA exercise

## Author of solution

Steven Hall

### UML Solution

```mermaid

classDiagram

IView       <.. Program
Model       <.. Program
Controller  <.. Program

IView       <|-- Model 
IView       <|..  View

IView       <|-- Controller
Model       <|-- Controller

class IView
<<interface>>  IView

```
