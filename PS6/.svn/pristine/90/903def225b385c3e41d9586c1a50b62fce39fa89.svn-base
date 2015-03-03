using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetUtilities;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;

namespace SS
{    
    /// <summary>
    /// This class represents a Spreadsheet object that extends the abstract 
    /// spreadsheet class
    /// </summary>
    /// <author>Basil Vetas</author>
    /// <date>10-1-2014</date>
    public class Spreadsheet : AbstractSpreadsheet
    {
        // dictionary maps cell name (row, col) to the cell itself
        Dictionary<string, Cell> cells;

        // dependency graph for cells
        DependencyGraph dg;
        
        // track if the spreadsheet was modified
        private bool changed;

        /// <summary>
        /// This zero-argument constructor creates a new empty spreadsheet
        /// 
        /// The constructor imposes no extra validity conditions
        /// It normalizes every cell name to itself
        /// It has version "default"
        /// </summary>        
        public Spreadsheet() : base(s => true, s => s, "default")
        {
            // initialize spreadsheet variables
            cells = new Dictionary<string, Cell>();
            dg = new DependencyGraph();

            // initialize changed to false
            Changed = false;
        }

        // ADDED FOR PS5
        /// <summary>
        /// Constructs a spreadsheet by recording its variable validity test,
        /// its normalization method, and its version information.  The variable validity
        /// test is used throughout to determine whether a string that consists of one or
        /// more letters followed by one or more digits is a valid cell name.  The variable
        /// equality test should be used thoughout to determine whether two variables are
        /// equal.
        /// </summary>
        /// <param name="isValid"></param>
        /// <param name="normalize"></param>
        /// <param name="version"></param>
        public Spreadsheet(Func<string, bool> isValid, Func<string, string> normalize, string version)
            : base(isValid, normalize, version)
        {
            // initialize spreadsheet variables
            cells = new Dictionary<string, Cell>();
            dg = new DependencyGraph();

            // initialize changed to false
            Changed = false;
        }

        // ADDED FOR PS5
        /// <summary>
        /// This constructor reads a saved spreadsheet from a file and uses it to construct 
        /// a new spreadsheet. It uses the delegate params to record its variable validity test,
        /// its normalization method, and its version information.  The variable validity
        /// test is used throughout to determine whether a string that consists of one or
        /// more letters followed by one or more digits is a valid cell name.  The variable
        /// equality test should be used thoughout to determine whether two variables are
        /// equal.
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="isValid"></param>
        /// <param name="normalize"></param>
        /// <param name="version"></param>
        public Spreadsheet(string filepath, Func<string, bool> isValid, Func<string, string> normalize, string version)
            : base(isValid, normalize, version)
        {
            // initialize spreadsheet variables
            cells = new Dictionary<string, Cell>();
            dg = new DependencyGraph();

            if (!(GetSavedVersion(filepath).Equals(version)))
                throw new SpreadsheetReadWriteException("The version of the file does not match the version parameter");

            // uses the 'ReadFile' helper method found below
            ReadFile(filepath, false); // read the entire 'filepath' not just the version info 

            // initialize changed to false
            Changed = false;
        }

        // ADDED FOR PS5
        /// <summary>
        /// True if this spreadsheet has been modified since it was created or saved                  
        /// (whichever happened most recently); false otherwise.
        /// </summary>
        public override bool Changed
        {
            get 
            {
                return changed;
            }
            protected set 
            {
                changed = value;
            }

        }

        // ADDED FOR PS5
        /// <summary>
        /// Returns the version information of the spreadsheet saved in the named file.
        /// If there are any problems opening, reading, or closing the file, the method
        /// should throw a SpreadsheetReadWriteException with an explanatory message.
        /// </summary>
        public override string GetSavedVersion(string filename)
        {            
            // uses the 'ReadFile' helper method found below
            return ReadFile(filename, true); // read the 'filename' and only ask for version information           
        }

        // ADDED FOR PS5
        /// <summary>
        /// Writes the contents of this spreadsheet to the named file using an XML format.
        /// The XML elements should be structured as follows:
        /// 
        /// <spreadsheet version="version information goes here">
        /// 
        /// <cell>
        /// <name>
        /// cell name goes here
        /// </name>
        /// <contents>
        /// cell contents goes here
        /// </contents>    
        /// </cell>
        /// 
        /// </spreadsheet>
        /// 
        /// There should be one cell element for each non-empty cell in the spreadsheet.  
        /// If the cell contains a string, it should be written as the contents.  
        /// If the cell contains a double d, d.ToString() should be written as the contents.  
        /// If the cell contains a Formula f, f.ToString() with "=" prepended should be written as the contents.
        /// 
        /// If there are any problems opening, writing, or closing the file, the method should throw a
        /// SpreadsheetReadWriteException with an explanatory message.
        /// </summary>
        public override void Save(string filename)
        {
            // if the spreadsheet hasn't been changed, we don't need to save anything
            //if (Changed == false)
            //    return;

            // the filename can't be null
            if (ReferenceEquals(filename, null))
                throw new SpreadsheetReadWriteException("The filename cannot be null");

            // the filename can't be empty
            if (filename.Equals(""))
                throw new SpreadsheetReadWriteException("The filename cannot be empty");
            
            try
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;

                // for some reason more tests fail when I don't set indent = true
                using (XmlWriter writer = XmlWriter.Create(filename, settings))
                {
                    writer.WriteStartDocument(); // start document
                    writer.WriteStartElement("spreadsheet"); // open spreadsheet tag                
                    writer.WriteAttributeString("version", null, Version);
                    foreach (string cell in cells.Keys)
                    {
                        writer.WriteStartElement("cell");   // open cell tag                        
                        writer.WriteElementString("name", cell);   // open name tag                                               

                        string cell_contents; // will hold the contents of this cell
                        // check the type of the contents of the cell
                        if (cells[cell].contents is double)
                        {   //  if the contents of this cell is a double
                            //  save the contents as a double.ToString();                        
                            cell_contents = cells[cell].contents.ToString();
                        }
                        else if (cells[cell].contents is Formula)
                        {   //  if the contents of this cell is a Formula
                            //  prepend "=" and save the contents as a Formula.ToString();
                            cell_contents = "=" + cells[cell].contents.ToString();
                        }
                        else
                        {   //  otherwise the contents of this cell is a string
                            //  save the contents as the string;
                            cell_contents = (string)cells[cell].contents;
                        }
                        
                        writer.WriteElementString("contents", cell_contents);   // open contents tag                        
                        writer.WriteEndElement(); // close cell tag
                    }
                    writer.WriteEndElement();               // close spreadsheet tag
                    writer.WriteEndDocument(); // end document  

                } // END Using
            } // END try
            catch (XmlException e)
            {
                throw new SpreadsheetReadWriteException(e.ToString());
            }
            catch(IOException e)
            {
                throw new SpreadsheetReadWriteException(e.ToString());
            }

            // after saving, set changed to false
            Changed = false;
        }

        // ADDED FOR PS5
        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, returns the value (as opposed to the contents) of the named cell.  The return
        /// value should be either a string, a double, or a SpreadsheetUtilities.FormulaError.
        /// </summary>
        public override object GetCellValue(string name)
        {
            // if name is null or invalid, throw exception
            if (ReferenceEquals(name, null) || !(IsValidName(name)))
                throw new InvalidNameException();

            Cell cell; // value of name

            // Otherwise return the value of the named cell
            if (cells.TryGetValue(name, out cell))
                return cell.value;
            else
                return "";
        }

        // ADDED FOR PS5
        /// <summary>
        /// If content is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, if content parses as a double, the contents of the named
        /// cell becomes that double.
        /// 
        /// Otherwise, if content begins with the character '=', an attempt is made
        /// to parse the remainder of content into a Formula f using the Formula
        /// constructor.  There are then three possibilities:
        /// 
        ///   (1) If the remainder of content cannot be parsed into a Formula, a 
        ///       SpreadsheetUtilities.FormulaFormatException is thrown.
        ///       
        ///   (2) Otherwise, if changing the contents of the named cell to be f
        ///       would cause a circular dependency, a CircularException is thrown.
        ///       
        ///   (3) Otherwise, the contents of the named cell becomes f.
        /// 
        /// Otherwise, the contents of the named cell becomes content.
        /// 
        /// If an exception is not thrown, the method returns a set consisting of
        /// name plus the names of all other cells whose value depends, directly
        /// or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override ISet<string> SetContentsOfCell(string name, string content)
        {
            // the content we want to set in a cell can't be null
            if (ReferenceEquals(content, null))
                throw new ArgumentNullException();

            // the name of the cell we want to set can't be null, and must be a valid name
            if (ReferenceEquals(name, null) || !(IsValidName(name)))
                throw new InvalidNameException();

            // holds the list of dependees to be returned from the correct SetCellContents method
            HashSet<String> all_dependents; 

            double result;  // will hold content if it is a double, otherwise remains unused

            // if content is empty, just add it to the cell
            if(content.Equals(""))
                all_dependents = new HashSet<String>(SetCellContents(name, content));
            // if we can parse content as a double, set the named cell contents to result
            else if (Double.TryParse(content, out result))
                all_dependents = new HashSet<String>(SetCellContents(name, result));
            // otherwise, if content begins with '=' then try to parse it as a formula
            else if (content.Substring(0, 1).Equals("="))
            {
                // try to create a formula from the remaining content (minus first '=' character)
                string formula_as_string = content.Substring(1, content.Length-1);

                // 1. If content can't be parsed as a Formula this will throw a FormulaFormatException
                Formula f = new Formula(formula_as_string, Normalize, IsValid);

                // 2. If this creates a circular dependency, will throw a CircularException
                // 3. Otherwise, the contents of 'name' become f
                all_dependents = new HashSet<String>(SetCellContents(name, f));
            }
            else // otherwise, the content is a string so just set the cell to that string
                all_dependents = new HashSet<String>(SetCellContents(name, content));

            // Loop here to re-evaluate cells in all_dependees;

            // after changing cell content, set changed to true
            Changed = true;

            // for each dependent, re-evaluate it based on the new dependee value
            foreach (string s in all_dependents)
            {
                Cell cell_value; // value associated with key
                // try to get the key, if we find it, re-evaluate it
                if(cells.TryGetValue(s, out cell_value))
                    cell_value.ReEvaluate(LookupValue);
            }
                                                              
            return all_dependents;   // return list of all the dependees of the cell            
        }

        /// <summary>
        /// Enumerates the names of all the non-empty cells in the spreadsheet.
        /// </summary>
        public override IEnumerable<String> GetNamesOfAllNonemptyCells()
        {
            // cell.Keys should contain the names of all non-empty cells
            return cells.Keys;
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, returns the contents (as opposed to the value) of the named cell.  The return
        /// value should be either a string, a double, or a Formula.
        /// </summary>
        public override object GetCellContents(String name)
        {
            if (ReferenceEquals(name, null) || !(IsValidName(name)))
                throw new InvalidNameException();

            Cell cell; // value of name

            name = Normalize(name); // normalize the cell name

            if (cells.TryGetValue(name, out cell))
                return cell.contents;
            else
                return "";
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes number.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        protected override ISet<String> SetCellContents(String name, double number)
        {
            // Dead code due to requirements for SetContentsOfCell
            //if (ReferenceEquals(name, null) || !(IsValidName(name)))
            //    throw new InvalidNameException();

            // Create a new cell
            Cell cell = new Cell(number);
            if (cells.ContainsKey(name))    // if it already contains that key
                cells[name] = cell;         // replace the key with the new value
            else
                cells.Add(name, cell);      // otherwise add a new key for that value

            // replace the dependents of 'name' in the dependency graph with an empty hash set
            dg.ReplaceDependees(name, new HashSet<String>());

            // recalculate at end
            HashSet<String> all_dependees = new HashSet<String>(GetCellsToRecalculate(name));
            return all_dependees;
        }

        /// <summary>
        /// If text is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes text.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        protected override ISet<String> SetCellContents(String name, String text)
        {
            // Dead code due to requirements for SetContentsOfCell
            //if (ReferenceEquals(text, null))
            //    throw new ArgumentNullException();

            // Dead code due to requirements for SetContentsOfCell
            //if (ReferenceEquals(name, null) || !(IsValidName(name)))
            //    throw new InvalidNameException();

            // Create a new cell
            Cell cell = new Cell(text);
            if (cells.ContainsKey(name))    // if it already contains that key
                cells[name] = cell;         // replace the key with the new value
            else
                cells.Add(name, cell);      // otherwise add a new key for that value

            string cell_content = (string) cells[name].contents; // cast is since we know it is a string

            if (cell_content.Equals("")) // if the contents is an empty string, we don't want it in the dictionary
                cells.Remove(name);

            // replace the dependents of 'name' in the dependency graph with an empty hash set
            dg.ReplaceDependees(name, new HashSet<String>());

            // recalculate at end
            HashSet<String> all_dependees = new HashSet<String>(GetCellsToRecalculate(name));
            return all_dependees;
        }

        /// <summary>
        /// If the formula parameter is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, if changing the contents of the named cell to be the formula would cause a 
        /// circular dependency, throws a CircularException.  (No change is made to the spreadsheet.)
        /// 
        /// Otherwise, the contents of the named cell becomes formula.  The method returns a
        /// Set consisting of name plus the names of all other cells whose value depends,
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        protected override ISet<String> SetCellContents(String name, Formula formula)
        {
            // Dead code due to requirements for SetContentsOfCell
            //if (ReferenceEquals(formula, null))
                //throw new ArgumentNullException();

            // Dead code due to requirements for SetContentsOfCell
            //(ReferenceEquals(name, null) || !(IsValidName(name)))
            //    throw new InvalidNameException();

            // temp variable to hold old dependents 
            IEnumerable<String> old_dependees = dg.GetDependees(name);

            // replace the dependents of 'name' in the dependency graph with the variables in formula
            dg.ReplaceDependees(name, formula.GetVariables());

            try // check if the new depdendency graph creates a circular reference
            {
                // if there is no exception
                HashSet<String> all_dependees = new HashSet<String>(GetCellsToRecalculate(name));
                // create a new cell
                Cell cell = new Cell(formula, LookupValue);
                if (cells.ContainsKey(name))    // if it already contains that key
                    cells[name] = cell;         // replace the key with the new value
                else
                    cells.Add(name, cell);      // otherwise add a new key for that value

                return all_dependees;
            }
            catch (CircularException e) // if an exception is caught, we want to keep the old dependents and not change the cell
            {
                dg.ReplaceDependees(name, old_dependees);
                throw new CircularException();
            }

        }

        /// <summary>
        /// If name is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name isn't a valid cell name, throws an InvalidNameException.
        /// 
        /// Otherwise, returns an enumeration, without duplicates, of the names of all cells whose
        /// values depend directly on the value of the named cell.  In other words, returns
        /// an enumeration, without duplicates, of the names of all cells that contain
        /// formulas containing name.
        /// 
        /// For example, suppose that
        /// A1 contains 3
        /// B1 contains the formula A1 * A1
        /// C1 contains the formula B1 + A1
        /// D1 contains the formula B1 - C1
        /// The direct dependents of A1 are B1 and C1
        /// </summary>
        protected override IEnumerable<String> GetDirectDependents(String name)
        {
            if (ReferenceEquals(name, null))
                throw new ArgumentNullException();

            if (!(IsValidName(name)))
                throw new InvalidNameException();

            // GetDependents returns a HashSet ensuring there won't be duplicates
            return dg.GetDependents(name); // changed this from GetDependees to GetDependents and fixed most of my tests  
        }

        /// <summary>
        /// Private helper method to check if the name of a cell is valid or not
        /// </summary>
        /// <param name="name"></param>
        private Boolean IsValidName(String name)
        {   
            // remove/add $ at end of Regex
            // if it is a valid cell name, and returns true by 'IsValid' delegate, return true, else return false            
            if (Regex.IsMatch(name, @"^[a-zA-Z]+[\d]+$") && IsValid(name)) 
                return true;
            else return false;
        }

        /// <summary>
        /// Private helper method to read in a spreadsheet from an xml file
        /// </summary>
        /// <param name="filename">the filename we are reading the spreadsheet from</param>
        /// <param name="only_get_version">true if we only want the verison of the spreadsheet, false otherwise</param>
        private string ReadFile(string filename, bool only_get_version)
        {
            if (ReferenceEquals(filename, null))
                throw new SpreadsheetReadWriteException("The filename cannot be null");

            if (filename.Equals(""))
                throw new SpreadsheetReadWriteException("The filename cannot be empty");

            try
            {
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    string name = "";       // the name of a given cell
                    string contents = "";   // the contents of the corresponding cell

                    while (reader.Read())   // while reader has remaining elements                    
                    {                                                
                        if (reader.IsStartElement())    // if reader is an opening tag
                        {
                            bool set_contents = false;
                            
                            switch (reader.Name)
                            {
                                case "spreadsheet":
                                    if (only_get_version) // if we only want the version information
                                        return reader["version"];   // return it
                                    else
                                        Version = reader["version"];                                       
                                    break;
                                case "cell":
                                    if (only_get_version) // should never be true
                                        throw new SpreadsheetReadWriteException("Error: Version should have already been returned");                                    
                                    break;
                                case "name":
                                    if (only_get_version) // should never be true
                                        throw new SpreadsheetReadWriteException("Error: Version should have already been returned");
                                    reader.Read();
                                    name = reader.Value;
                                    break;
                                case "contents":
                                    if (only_get_version) // should never be true
                                        throw new SpreadsheetReadWriteException("Error: Version should have already been returned");
                                    reader.Read();
                                    contents = reader.Value;
                                    set_contents = true;
                                    break;
                            } // end switch

                            if(set_contents)
                                SetContentsOfCell(name, contents);

                        } // end if                        
                    } // end while
                } // end using
            } // end try
            catch(XmlException e)
            {
                throw new SpreadsheetReadWriteException(e.ToString());
            }
            catch(IOException e)
            {
                throw new SpreadsheetReadWriteException(e.ToString());
            }

            return Version;
        }

        /// <summary>
        ///     This class creates a cell object 
        /// </summary>
        private class Cell
        {
            // only one of these conctructors will be initialized
            public Object contents { get; private set; }
            public Object value { get; private set; }

            // the data type of the contents and value
            string contents_type;
            string value_type;

            /// <summary>
            ///     Constructor for strings
            /// </summary>
            /// <param name="name">The name of the cell</param>
            public Cell(string name)
            {
                contents = name;
                value = contents;
                contents_type = name.GetType().ToString();
                value_type = contents_type;
            }

            /// <summary>
            ///     Constructor for doubles
            /// </summary>
            /// <param name="name">The name of the cell</param>
            public Cell(double name)
            {
                contents = name;
                value = contents;
                contents_type = name.GetType().ToString();
                value_type = contents_type;
            }

            /// <summary>
            ///     Constructor for Formulas
            /// </summary>
            /// <param name="name">The name of the cell</param>
            /// <param name="lookup">The lookup method for the cell</param>
            public Cell(Formula name, Func<string, double> lookup)
            {    
                contents = name;                
                value = name.Evaluate(lookup);
                contents_type = name.GetType().ToString();
                value_type = value.GetType().ToString();
            }            

            /// <summary>
            ///     Helper method for re-evaluating formulas when their dependees 
            ///     are changed. Used in the SetContentsOfCell method. This 
            ///     method should only be used on cells that have a Formula as
            ///     their contents. 
            /// </summary>
            /// <param name="lookup">Lookup delegate for value</param>
            public void ReEvaluate(Func<string, double> lookup)
            {
                if(contents_type.Equals("SpreadsheetUtilities.Formula"))
                {
                    Formula same = (Formula)contents;
                    value = same.Evaluate(lookup);
                }                
            }

        } // END Cell class
        
        /// <summary>
        ///     Helper method for evaluating functions. This will return the value
        ///     associated with a given cell name.          
        /// </summary>
        /// <param name="s">The cell name to be looked up</param>
        /// <returns>The value of the cell named 's'</returns>
        private double LookupValue(string s)
        {            
            Cell cell; // value of name

            // if the dictionary contains the key name 's'
            if (cells.TryGetValue(s, out cell))
            {
                // check if 'cell' is a double                                
                if (cell.value is double)
                    return (double)cell.value;
                else // if it is not throw an exception
                    throw new ArgumentException();                  
            }
            else // if it does not contain 's' throw an exception
               throw new ArgumentException();

        } // END LoopupValue class

    } // END Spreadsheet class

} // END SS Namespace
