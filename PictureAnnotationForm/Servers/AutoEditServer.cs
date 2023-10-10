using Newtonsoft.Json.Bson;
using PictureAnnotationForm.Attributes;
using PictureAnnotationForm.Enums;
using PictureAnnotationForm.Forms;
using PictureAnnotationForm.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureAnnotationForm.Servers
{
    public class AutoEditServer
    {
        private static Type _ptype = typeof(Propertys);
        private Dictionary<Type, Control> _controlDict = new Dictionary<Type, Control>();
        public Control Control;
        public object Data { get; private set; }
        public Type DataType { get; private set; }
        public void Init(object obj, Type objType,Control control)
        {
            Data = obj;
            DataType = objType;
            Control = control;
        }
        private void UpdateData(object obj, MemberInfoPropertysBind memberInfo)
        {
            if (memberInfo.Control != null)
            {
                switch (memberInfo.Propertys.Type)
                {
                    case EPropertysType.Int:
                    case EPropertysType.Decimal:
                        memberInfo.Property.SetValue(obj, ConvertValue(memberInfo.Property.PropertyType, memberInfo.Control.Text));
                        return;
                    case EPropertysType.Text:
                        memberInfo.Property.SetValue(obj, memberInfo.Control.Text);
                        return;
                }

            }
            if (memberInfo.Property != null)
            {
                obj = memberInfo.Property.GetValue(obj);
            }
            foreach (var propertysBind in memberInfo.Child)
            {
                UpdateData(obj, propertysBind);
            }
        }
        private object ConvertValue(Type type, string text)
        {
            switch (type.Name)
            {
                case "Int":
                    return int.Parse(text);
                case "Double":
                    return double.Parse(text);
            }
            return text;
        }
        private void SetControl(MemberInfoPropertysBind memberInfoPropertysBind, Control control)
        {
            int x = 0, y = 5;
            foreach (var bind in memberInfoPropertysBind.Child)
            {
                if (bind.Child.Count > 0)
                {
                    GroupBox groupBox = new GroupBox();
                    groupBox.Width = control.Width - 10;
                    SetControl(bind, groupBox);
                    groupBox.Top = y + 5;
                    groupBox.Left = 5;
                    y += groupBox.Height + 5;
                    control.Controls.Add(groupBox);
                }
                else
                {
                    Label label = new Label();
                    label.Text = (string.IsNullOrWhiteSpace(bind.Propertys.Text) ? bind.Property.Name : bind.Propertys.Text) + ":";
                    label.Top += y + 5;
                    label.Left = 5;
                    y += 35;
                    control.Controls.Add(label);
                    GetEPropertysType(bind);
                    if (bind.Propertys.Type == EPropertysType.Text)
                    {
                        TextBox textBox = new TextBox();
                        textBox.Top = label.Top;
                        textBox.Height = 27;
                        textBox.Left = 170;
                        textBox.Width = control.Width - 175;
                        textBox.Text = bind.Value.ToString();
                        control.Controls.Add(textBox);
                        bind.Control = textBox;
                    }
                    else if (bind.Propertys.Type == EPropertysType.Enum)
                    {
                        ComboBox comboBox = new ComboBox();
                        comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                        comboBox.Top = label.Top;
                        comboBox.Height = 27;
                        comboBox.Left = 170;
                        comboBox.Width = control.Width - 175;
                        comboBox.Items.AddRange(bind.Property.PropertyType.GetEnumNames());
                        comboBox.Text = bind.Value.ToString();
                        control.Controls.Add(comboBox);
                        bind.Control = comboBox;
                    }
                    else if (bind.Propertys.Type == EPropertysType.Int || bind.Propertys.Type == EPropertysType.Decimal)
                    {
                        NumericUpDown numericUp = new NumericUpDown();
                        numericUp.Top = label.Top;
                        numericUp.Height = 27;
                        numericUp.Left = 170;
                        numericUp.Width = control.Width - 175;
                        numericUp.Text = bind.Value.ToString();
                        if (bind.Propertys.Type == EPropertysType.Int)
                        {
                            numericUp.DecimalPlaces = 0;
                        }
                        else
                        {
                            numericUp.DecimalPlaces = 2;
                            numericUp.Increment = 0.01m;
                        }
                        control.Controls.Add(numericUp);
                        bind.Control = numericUp;
                    }
                    else if (bind.Propertys.Type == EPropertysType.Folder)
                    {
                        TextBox textBox = new TextBox();
                        textBox.Top = label.Top;
                        textBox.Height = 27;
                        textBox.Left = 170;
                        textBox.Width = control.Width - 175 - 40;
                        textBox.Text = bind.Value.ToString();
                        control.Controls.Add(textBox);
                        bind.Control = textBox;
                        Button button = new Button();
                        button.Text = "...";
                        button.Top = textBox.Top;
                        button.Width = 35;
                        button.Left = control.Width - 40;
                        button.Click += (s, e) =>
                        {
                            var folderBrowserDialog = GetControl<FolderBrowserDialog>();
                            if (autoEditForm.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                            {
                                textBox.Text = autoEditForm.folderBrowserDialog1.SelectedPath;
                            }
                        };
                        control.Controls.Add(button);
                    }

                }
            }
            control.Height = y + 5;
        }
        private  T? GetControl<T>() where T : Control
        {
            var type= typeof(T);
            if (_controlDict.ContainsKey(type))
            {
                return _controlDict[type] as T;
            }
            Form form = Control.FindForm();
            if (form != null)
            {
                foreach (Control c in form.Controls)
                {
                    if(c.GetType().FullName== type.FullName)
                    {
                        _controlDict.Add(type,c);
                        return c as T;
                    }
                }
            }
            Control control = Control;
            while (control != null)
            {
                foreach (Control c in control.Controls)
                {
                    if (c.GetType().FullName == type.FullName)
                    {
                        _controlDict.Add(type, c);
                        return c as T;
                    }
                }
                control = control.Parent;
            }
            return null;
        }
        private void GetEPropertysType(MemberInfoPropertysBind memberInfoPropertysBind)
        {
            if (memberInfoPropertysBind.Propertys.Type != EPropertysType.Atuo)
            {
                return;
            }

            if (memberInfoPropertysBind.Property.PropertyType.IsEnum)
            {
                memberInfoPropertysBind.Propertys.Type = EPropertysType.Enum;
                return;
            }

            switch (memberInfoPropertysBind.Property.PropertyType.Name)
            {
                case "Double":
                case "Decimal":
                    memberInfoPropertysBind.Propertys.Type = EPropertysType.Decimal;
                    return;
                case "String":
                    memberInfoPropertysBind.Propertys.Type = EPropertysType.Text;
                    return;
                case "Int":
                    memberInfoPropertysBind.Propertys.Type = EPropertysType.Int;
                    return;
            }

            memberInfoPropertysBind.Propertys.Type = EPropertysType.Text;
        }

        private MemberInfoPropertysBind InfoPropertysBind(object obj, MemberInfoPropertysBind bind)
        {
            Type type = obj.GetType();
            if (bind == null)
            {
                bind = new MemberInfoPropertysBind
                {
                    Value = obj,
                    Propertys = GetPropertys(type),
                };
            }
            foreach (var property in type.GetProperties())
            {
                var propertys = GetPropertys(property);
                if (propertys == null)
                {
                    continue;
                }
                if (property.PropertyType.IsCollectible)
                {
                    Debugger.Break();
                }
                else if (property.PropertyType.IsClass && !property.PropertyType.IsSealed)
                {
                    var mipb = new MemberInfoPropertysBind
                    {
                        Value = obj == null ? propertys.Value : property.GetValue(obj),
                        Property = property,
                        Propertys = propertys,
                        Parents = bind,
                    };
                    bind.Child.Add(mipb);
                    InfoPropertysBind(mipb.Value, mipb);
                    bind.AllItemNum += mipb.AllItemNum;
                }
                else
                {
                    bind.Child.Add(new MemberInfoPropertysBind
                    {
                        Value = obj == null ? propertys.Value : property.GetValue(obj),
                        Property = property,
                        Propertys = propertys,
                        Parents = bind,
                    });
                    bind.AllItemNum++;
                }
            }
            return bind;
        }
        private  Propertys GetPropertys(MemberInfo memberInfo)
        {
            var ps = memberInfo.GetCustomAttributes(_ptype, false);
            if (ps.Length > 0)
            {
                return ps[0] as Propertys;
            }
            else
            {
                return null;
            }
        }
    }
}
