﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8bitVonNeiman.Compiler.View {
    public interface CompilerFormOutput {
        void FormClosed(string code);
        void Compile(string code);
    }
}
