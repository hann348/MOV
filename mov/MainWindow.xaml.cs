using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace mov
{
    public partial class MainWindow : Window
    {
        private string[] registers = {"0000", "0000", "0000", "0000"};
        private string[] memory = {"0000", "1111", "2222", "3333", "4444", "5555", "AAAA", "BBBB", "CCCC", "DDDD"};
        private string[] mem_addresses = {"1100", "1101", "1102", "1103", "1104", "1105", "1106", "1107", "1108", "1109"};


        public void print_regs_and_memory()
        {
            this.AX.Text = "AX: " + registers[0];
            this.BX.Text = "BX: " + registers[1];
            this.CX.Text = "CX: " + registers[2];
            this.DX.Text = "DX: " + registers[3];

            this.mem_board.Text = "memory:\n(address, value)\n\n";
            for (int ctr = 0; ctr < 10; ctr++)
            {
                this.mem_board.AppendText(Convert.ToString(1100 + ctr) + "  " + memory[ctr] + "\n");
            }

        }

        public int validate_register_name(string given_name)
        {
            if (given_name.ToLower() == "ax") return 0;
            if (given_name.ToLower() == "bx") return 1;
            if (given_name.ToLower() == "cx") return 2;
            if (given_name.ToLower() == "dx") return 3;

            return -1;
        }

        public int validate_mem_address(string given_address)
        {
            for(int ctr=0; ctr<10; ctr++)
            {
                if (given_address == mem_addresses[ctr]) return ctr;
            }
   
            return -1;
        }


        public MainWindow()
        {
            InitializeComponent();
        }

        private void win_loaded(object sender, RoutedEventArgs e)
        {
            print_regs_and_memory();
        }

        private void show_me_your_moves(object sender, RoutedEventArgs e)
        {
          

            int op1_reg_ind, op1_mem_ind, op2_reg_ind, op2_mem_ind, op2_imm;
            op1_reg_ind = op1_mem_ind = op2_reg_ind = op2_mem_ind = op2_imm = -1;

            if (this.op1_reg.IsChecked == true)
            {
                op1_reg_ind = validate_register_name(this.op1_val.Text);
            }
            else
            {
                op1_mem_ind = validate_mem_address(this.op1_val.Text);
            }

            if (this.op2_reg.IsChecked == true)
            {
                op2_reg_ind = validate_register_name(this.op2_val.Text);
            }
            if (this.op2_mem.IsChecked == true)
            {
                op2_mem_ind = validate_mem_address(this.op2_val.Text);
            }
            if (this.op2_imm.IsChecked == true)
            {
                op2_imm = 1;
            }

            // register -> register
            if (op1_reg_ind >= 0 && op2_reg_ind >= 0)
            {
                registers[op1_reg_ind] = registers[op2_reg_ind];
            }

            // register -> memory
            if (op1_mem_ind >= 0 && op2_reg_ind >= 0)
            {
                memory[op1_mem_ind] = registers[op2_reg_ind];
            }

            // memory -> register
            if (op1_reg_ind >= 0 && op2_mem_ind >= 0)
            {
                registers[op1_reg_ind] = memory[op2_mem_ind];
            }

            // immediate -> register
            if (op1_reg_ind >= 0 && op2_imm >= 0)
            {
                registers[op1_reg_ind] = this.op2_val.Text;
            }

            // immediate -> memory
            if (op1_mem_ind >= 0 && op2_imm >= 0)
            {
                memory[op1_mem_ind] = this.op2_val.Text;
            }



            print_regs_and_memory();
        }
    }
}
